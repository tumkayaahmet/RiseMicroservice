using Shared.Dtos;
namespace Report.Models
{
    public class Person : BaseClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public List<ContactInformation> ContactInformation { get; set; }
    }
}
