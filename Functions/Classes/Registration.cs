using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pantupfunctions.Classes
{
    public class Registration
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("created")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; } = DateTime.Now;

        [BsonElement("associationLink")]
        public string AssociationLink { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("zipcode")]
        public string ZipCode { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("numLargeBags")]
        public int? NumLargeBags { get; set; } = 0;

        [BsonElement("numSmallBags")]
        public int? NumSmallBags { get; set; } = 0;

        [BsonElement("state")]
        public int? State { get; set; }

        [BsonElement("confirmedDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ConfirmedDate { get; set; }

        [BsonElement("pickUpDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? PickUpDate { get; set; }

        [BsonElement("pickedUpDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? PickedUpDate { get; set; }

        [BsonElement("closedDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ClosedDate { get; set; }

        [BsonElement("numLargeBagsPickedUp")]
        public int? NumLargeBagsPickedUp { get; set; } = 0;

        [BsonElement("numSmallBagsPickedUp")]
        public int? NumSmallBagsPickedUp { get; set; } = 0;

        [BsonElement("notes")]
        public string Notes { get; set; }

        [BsonElement("mobile")]
        public string Mobile { get; set; }

    }
}
