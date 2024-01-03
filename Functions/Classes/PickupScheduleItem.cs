using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pantupfunctions.Classes
{
    public class PickupScheduleItem
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("associationLink")]
        public string AssociationLink { get; set; }

        [BsonElement("pickupDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? PickupDate { get; set; }
    }
}
