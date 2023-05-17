using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.ControllerBases;
using Shared.Dtos;

namespace Contact.Models
{
    public class Person : BaseClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public List<ContactInformation> ContactInformation { get; set; }
    }
}
