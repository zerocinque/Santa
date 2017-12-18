using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public List<Toy> Toys { get; set; }

        [BsonElement("requestDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DisplayName("Request date")]
        public DateTime RequestDate { get; set; }

        [BsonIgnore]
        public double TotalAmount { get; set; }

        [BsonIgnore]
        public bool IsPreparable { get; set; }

        public void CountTotalAmount()
        {
            double total = 0;
            foreach(Toy toy in this.Toys)
            {
                total += toy.Price;
            }
            TotalAmount = total;
        }

        public string ToysToString()
        {
            string msg = "";
            foreach(Toy toy in Toys)
            {
                msg = string.Concat(msg, toy, "; ");
            }
            return msg;
        }
    }
}
