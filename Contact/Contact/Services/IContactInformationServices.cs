using Shared.Dtos;

namespace Contact.Services
{
    public interface IContactInformationServices
    {
        Task<Response<List<ContactInformationDto>>> GetAllByPersonIdAsync(string personId);
        Task<Response<ContactInformationDto>> CreateAsync(ContactInformationCreateDto contactInformationCreateDto);
        Task<Response<NoContent>> UpdateAsync(ContactInformationUpdateDto contactInformationUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
