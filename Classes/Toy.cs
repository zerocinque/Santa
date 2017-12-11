using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Toy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("cost")]
        public string Price { get; private set; }

        [BsonElement("amount")]
        public int Amount { get; private set; }

    }
}
