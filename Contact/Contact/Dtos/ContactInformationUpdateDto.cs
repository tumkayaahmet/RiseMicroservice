using Contact.Models;

namespace Contact.Dtos
{
    public class ContactInformationUpdateDto : PropertyClass
    {
        public string Id { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InformationContent { get; set; }
        public string PersonId { get; set; }

    }
}
