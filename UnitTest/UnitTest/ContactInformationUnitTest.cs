using Contact.Controllers;
using Contact.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Dtos;
using Shared.Messages;

namespace UnitTest
{
    public class ContactInformationUnitTest
    {
        private Mock<IContactInformationServices> _contactInformationServices;

        public ContactInformationUnitTest()
        {
            _contactInformationServices = new Mock<IContactInformationServices>();
        }

        [Test]
        public void Contact_GetAllbyPersonIdAsync()
        {
            string personId = "64653482503e72de255cd832";
            var getContactInformation = _contactInformationServices.Setup(x => x.GetAllByPersonIdAsync(personId).Result).Returns(GetByPersonId);
            var contactInformationContoller = new ContactInformationController(_contactInformationServices.Object);
            IActionResult response = contactInformationContoller.GetAllbyPersonIdAsync(personId).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<List<ContactInformationDto>>;

            Assert.AreEqual(GetByPersonId().Data.Count, result.Data.Count);
        }

        [Test]
        public void Contact_ContactInformation_Create()
        {
            ContactInformationCreateDto contactInformationCreateDto = new()
            {
                CreateDate = DateTime.Now,
                Email = "ccc@ccc.com",
                InformationContent = "Phone",
                Location = "İstanbul",
                PhoneNumber = "05550000000",
                PersonId = "64653482503e72de255cd832"
            };
            var newPerson = _contactInformationServices.Setup(x => x.CreateAsync(contactInformationCreateDto).Result).Returns(CreatePerson);
            var personContoller = new ContactInformationController(_contactInformationServices.Object);
            IActionResult response = personContoller.Create(contactInformationCreateDto).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<ContactInformationDto>;
            Assert.IsTrue(result.Data.Location == CreatePerson().Data.Location && result.Data.PhoneNumber == CreatePerson().Data.PhoneNumber);
        }

        [Test]
        public void Contact_ContactInformation_Update()
        {
            ContactInformationUpdateDto contactInformationUpdateDto = new()
            {
                Id = "6465366a4545b6f53d7b2160",
                CreateDate = DateTime.Now,
                Email = "ccc@ccc.com",
                InformationContent = "Phone",
                Location = "Ankara",
                PhoneNumber = "05550000000",
                PersonId = "64653482503e72de255cd832"
            };
            ContactInformationDto contactInformationDto = new()
            {
                Id = "6465366a4545b6f53d7b2160",
                CreateDate = DateTime.Now,
                Email = "ccc@ccc.com",
                InformationContent = "Phone",
                Location = "Ankara",
                PhoneNumber = "05550000000",
                PersonId = "64653482503e72de255cd832"
            };

            _contactInformationServices.Setup(x => x.UpdateAsync(contactInformationUpdateDto).Result).Returns(Response<NoContent>.Success(StatusCodes.Status204NoContent));
            var contactInformationController = new ContactInformationController(_contactInformationServices.Object);
            IActionResult response = contactInformationController.Update(contactInformationUpdateDto).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<NoContent>;

            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Test]
        public void Contact_ContactInformation_Delete()
        {
            string contactInformationId = "6465366a4545b6f53d7b2160";
            _contactInformationServices.Setup(x => x.DeleteAsync(contactInformationId).Result).Returns(Response<NoContent>.Success(StatusCodes.Status204NoContent));
            var contactInformationController = new ContactInformationController(_contactInformationServices.Object);
            IActionResult response = contactInformationController.Delete(contactInformationId).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<NoContent>;

            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
        }


        private Response<List<ContactInformationDto>> GetByPersonId()
        {
            return Response<List<ContactInformationDto>>.Success(new List<ContactInformationDto>()
            {
                new ContactInformationDto()
                    {
                        Id = "646b204a0b2c4c95ae31e9dd",
                        CreateDate = DateTime.Now,
                        Email ="a@c.com",
                        InformationContent="Phone",
                        Location ="Antalya",
                        ModifyDate =DateTime.Now,
                        PhoneNumber="0545411111111",
                        PersonId="64653482503e72de255cd832"
                    },
                new ContactInformationDto()
                    {
                        Id = "646b204a0b2c4c95ae31e9dd",
                        CreateDate = DateTime.Now,
                        Email ="bb@bb.com",
                        InformationContent="Phone",
                        Location ="Antalya",
                        ModifyDate =DateTime.Now,
                        PhoneNumber="05320000000",
                        PersonId="64653482503e72de255cd832"
                    }
            }, ResponseMessages.DataCount + 2, 200);
        }

        private Response<ContactInformationDto> CreatePerson()
        {
            return Response<ContactInformationDto>.Success(new ContactInformationDto()
            {
                CreateDate = DateTime.Now,
                Email = "ccc@ccc.com",
                InformationContent = "Phone",
                Location = "İstanbul",
                PhoneNumber = "05550000000",
                PersonId = "64653482503e72de255cd832"
            }, 200);
        }

    }
}
