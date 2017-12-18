using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Santa.Classes
{
    public class Toy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cost")]
        public double Price { get; set; }

        [BsonElement("amount")]
        public int Amount { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
