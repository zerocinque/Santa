using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Santa.Classes
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("screenname")]
        public string ScreenName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
