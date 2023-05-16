namespace Contact.Dtos
{
    public class ContactInformationCreateDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InformationContent { get; set; }
        public string PersonId { get; set; }
    }
}
