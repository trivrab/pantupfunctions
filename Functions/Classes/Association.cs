using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pantupfunctions.Classes
{
    public class Association
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("responsibleName")]
        public string ResponsibleName { get; set; }

        [BsonElement("responsibleEmail")]
        public string ResponsibleEmail { get; set; }

        [BsonElement("responsiblePhone")]
        public string ResponsiblePhone { get; set; }

        [BsonElement("nextPickup")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? NextPickup { get; set; }

        [BsonElement("logoURL")]
        public string LogoURL { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }

        [BsonElement("logoBase64")]
        public string LogoBase64 { get; set; }

        [BsonElement("zipCodes")]
        public string ZipCodes { get; set; }

        [BsonElement("information")]
        public string Information { get; set; }

        [BsonElement("backgroundColor")]
        public string BackgroundColor { get; set; }

        [BsonElement("foregroundColor")]
        public string ForegroundColor { get; set; }

        [BsonElement("acceptMessage")]
        public string AcceptMessage { get; internal set; }

        [BsonElement("rejectMessage")]
        public string RejectMessage { get; internal set; }

        [BsonElement("registrationReminder")]
        public bool? RegistrationReminder { get; internal set; }
    }
}
