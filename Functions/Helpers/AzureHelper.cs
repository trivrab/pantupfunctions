using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;

namespace Pantupfunctions.Helpers
{
    public class AzureHelper
    {
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay= TimeSpan.FromSeconds(0),
                MaxDelay = TimeSpan.FromSeconds(3),
                MaxRetries = 5,
                Mode = RetryMode.Fixed
            }
        };

        public static KeyVaultSecret GetSecret(string key, string endpoint, string mikey)
        {

            var client = new SecretClient(new Uri(endpoint), new DefaultAzureCredential(
                                        new DefaultAzureCredentialOptions() { ManagedIdentityClientId = mikey }));
            KeyVaultSecret secret = client.GetSecret(key);
            return secret;
        }
    }
}
