using Contact.Models;
using Shared.Dtos;

namespace Contact.Services
{
    public interface IPersonServices
    {
        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> CreateAsync(PersonCreateDto person);
        Task<Response<PersonDto>> GetByIdAsync(string id);
        Task<Response<NoContent>> UpdateAsync(PersonUpdateDto personDto);
        Task<Response<NoContent>> DeleteAsync(string id); 
    }
}
