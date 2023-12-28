using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Linq;
using Pantupfunctions.Classes;
using Pantupfunctions.Helpers;

namespace Functions
{
    public class PickupScheduleReminderFunction
    {
        [FunctionName("PickupScheduleReminderFunction")]
        public void Run([TimerTrigger("%PickupScheduleReminderCron%"
#if DEBUG
            , RunOnStartup=true
#endif
            )]TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"Trigger started at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            //var dbConnstr = conf["DBCONNSTR"].ToString();
            var DBCONNSTR = EnvironmentHelper.GetEnvironmentVariable("DBCONNSTR");
            var SENDGRIDKEY = EnvironmentHelper.GetEnvironmentVariable("SENDGRID");
            var DBSTR = EnvironmentHelper.GetEnvironmentVariable("DATABASE");
            var AZUREENDPOINT = EnvironmentHelper.GetEnvironmentVariable("AzureKeyVaultEndpoint");
            var MIKEY = EnvironmentHelper.GetEnvironmentVariable("AzureMIKey");
            var TEMPLATEID = EnvironmentHelper.GetEnvironmentVariable("TemplateIdPickupSchedule");
            var FROMEMAIL = EnvironmentHelper.GetEnvironmentVariable("FromEmail");
            var FROMNAME = EnvironmentHelper.GetEnvironmentVariable("FromName");
            var REPORTTOEMAIL = EnvironmentHelper.GetEnvironmentVariable("ReportToEmail");
            var ENVIRONMENT = EnvironmentHelper.GetEnvironmentVariable("Environment");

            /** Initialize stuff**/
            var sendgridsecret = AzureHelper.GetSecret(SENDGRIDKEY, AZUREENDPOINT, MIKEY).Value;
            var dbsecret = AzureHelper.GetSecret(DBCONNSTR, AZUREENDPOINT, MIKEY).Value;

            var dbClient = new MongoClient(dbsecret);
            var db = dbClient.GetDatabase(DBSTR);

            var client = new SendGridClient(sendgridsecret);

            /** Get all associations **/
            List<Association> assocs = new List<Association>();
            var collection = db.GetCollection<Association>("associations");
            assocs = collection.Find(x => x.PickupScheduleReminder.HasValue && x.PickupScheduleReminder.Value).ToList();

            /** For each association, if registrations, send mail **/
            List<string> sends = new List<string>();
            var collectionpickup = db.GetCollection<PickupScheduleItem>("pickupschedules");

            foreach (var assoc in assocs)
            {
                try
                {
                    var nextpickup = collectionpickup.Find(x => x.AssociationLink.ToUpper() == assoc.Link.ToUpper()
                        && x.PickupDate >= DateTime.Now.AddDays(7)).Any();

                    if (nextpickup)
                    {
                        continue;
                    }

                    var from = new EmailAddress(FROMEMAIL, FROMNAME);
                    var to = new EmailAddress(assoc.ResponsibleEmail, assoc.ResponsibleName);
                    SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(from, to, TEMPLATEID,
                        new Dictionary<string, object> { { "assoc", assoc.Name } });
                    var resp = client.SendEmailAsync(msg).Result;
                    var cnt = resp.IsSuccessStatusCode;

                    sends.Add($"<li>{assoc.Name}</li>");

                    log.LogInformation($"Sent pickupschedule mail to: {assoc.ResponsibleEmail}");
                }
                catch (Exception)
                {
                    log.LogInformation($"Fail to send pickupschedule mail to: {assoc.ResponsibleEmail}");
                }
            }

            /** Send report **/
            try
            {
                SendGridMessage msg = MailHelper.CreateSingleEmail(new EmailAddress(FROMEMAIL), new EmailAddress(REPORTTOEMAIL),
                    $"Uppdatera upphämtningsschema påminnelse - Rapport ({ENVIRONMENT})", "", $"<p>Skickat till: </p><ul>{string.Join("", sends)}</ul>");
                var resp = client.SendEmailAsync(msg).Result;
                log.LogInformation($"Sent pickupschedule report mail to: andre@viebke87.se");

            }
            catch (Exception ex)
            {
                log.LogInformation($"Failed to send pickupschedule report mail");
            }

            log.LogInformation($"Trigger ended at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
