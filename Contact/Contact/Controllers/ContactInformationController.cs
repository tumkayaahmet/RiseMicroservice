using Contact.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Dtos;

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : CustomBaseController
    {
        private readonly IContactInformationServices _contactInformationServices;

        public ContactInformationController(IContactInformationServices contactInformationServices)
        {
            _contactInformationServices = contactInformationServices;
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetAllbyPersonIdAsync(string personId)
        {
            var response = await _contactInformationServices.GetAllByPersonIdAsync(personId);
            return CreateActionResultInstance(response);

        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactInformationCreateDto contactInformationCreateDto)
        {
            var response = await _contactInformationServices.CreateAsync(contactInformationCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContactInformationUpdateDto contactInformationUpdateDto)
        {
            var response = await _contactInformationServices.UpdateAsync(contactInformationUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _contactInformationServices.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }

    }
}
