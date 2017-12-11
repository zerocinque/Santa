using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Classes
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; private set; }

        [BsonElement("kid")]
        public string Kid { get; private set; }

        [BsonElement("status")]
        public OrderStatus Status { get; set; }

        [BsonElement("toys")]
        public IEnumerable<Object> Toys { get; private set; }

        [BsonElement("requestDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RequestDate { get; private set; }

    }
}
