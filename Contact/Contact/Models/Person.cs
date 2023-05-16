using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Contact.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
    }
}
