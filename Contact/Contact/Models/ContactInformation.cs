using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Contact.Models
{
    public class ContactInformation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InformationContent { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonId { get; set; }

        [BsonIgnore]
        public Person Person { get; set; }

    }
}
