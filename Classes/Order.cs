﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Santa.Classes
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("kid")]
        public string Kid { get; set; }

        [BsonElement("status")]
        public OrderStatus Status { get; set; }

        [BsonElement("toys")]
        public IEnumerable<Object> Toys { get; set; }

        [BsonElement("requestDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RequestDate { get; set; }

    }
}
