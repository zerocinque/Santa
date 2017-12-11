using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Santa.Classes
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; private set; }

        [BsonElement("screenname")]
        public string ScreenName { get; private set; }

        [BsonElement("email")]
        public string Email { get; private set; }

        [BsonElement("isAdmin")]
        public string IsAdmini { get; private set; }

        [BsonElement("password")]
        public string Password { get; private set; }
    }
}
