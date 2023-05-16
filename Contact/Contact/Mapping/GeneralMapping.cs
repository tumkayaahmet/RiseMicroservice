using AutoMapper;
using Contact.Dtos;
using Contact.Models;

namespace Contact.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Person,PersonDto>().ReverseMap();
            CreateMap<ContactInformation,ContactInformationDto>().ReverseMap();

            CreateMap<Person, PersonCreateDto>().ReverseMap();
            CreateMap<Person, PersonUpdateDto>().ReverseMap();
            CreateMap<ContactInformation, ContactInformationCreateDto>().ReverseMap();
            CreateMap<ContactInformation, ContactInformationUpdateDto>().ReverseMap();
        }
    }
}
