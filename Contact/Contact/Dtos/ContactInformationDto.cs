using Contact.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Contact.Dtos
{
    public class ContactInformationDto : PropertyClass
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InformationContent { get; set; }
        public string PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}
