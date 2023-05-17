using Contact.Models;

namespace Contact.Dtos
{
    public class PersonCreateDto : PropertyClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }

    }
}
