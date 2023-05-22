using Contact.Controllers;
using Contact.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Dtos;

namespace UnitTest
{
    public class ContactPersonUnitTest
    {
        private Mock<IPersonServices> _personServices;

        public ContactPersonUnitTest()
        {
            _personServices = new Mock<IPersonServices> ();
        }

        [Test]
        public void Person_GetAll()
        {
            var list = _personServices.Setup(x => x.GetAllAsync().Result).Returns(GetPersons);
            var personContoller = new PersonController(_personServices.Object);
            IActionResult response = personContoller.GetAll().GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<List<PersonDto>>;

            Assert.AreEqual(GetPersons().Data.Count, result.Data.Count);
        }

        [Test]
        public void Person_Create()
        {
            PersonCreateDto person = new()
            {
                CreateDate = DateTime.Now,
                Firm = "ABC LTD. ŞTİ.",
                Name = "Ali",
                Surname = "Veli"

            };

            var newPerson = _personServices.Setup(x => x.CreateAsync(person).Result).Returns(CreatePerson());
            var personContoller = new PersonController(_personServices.Object);
            IActionResult response = personContoller.Create(person).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<PersonDto>;
            Assert.IsTrue(result.Data.Name == CreatePerson().Data.Name && result.Data.Surname == CreatePerson().Data.Surname);

         }

        [Test]
        public void Person_GetById()
        {
            string personId = "64653482503e72de255cd832";
            var getPerson = _personServices.Setup(x => x.GetByIdAsync(personId).Result).Returns(GetById);
            var personContoller = new PersonController(_personServices.Object);
            IActionResult response = personContoller.GetById(personId).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<PersonDto>;

            Assert.IsTrue(result.Data.Id == GetById().Data.Id);
        }

        [Test]
        public void Person_Update()
        {
            PersonUpdateDto personUpdate = new()
            {
                CreateDate = DateTime.Now,
                Id = "64653482503e72de255cd832",
                Firm = "Test Update AŞ."
            };

            var updatedPerson = new PersonDto()
            {
                CreateDate = DateTime.Now,
                Id = "64653482503e72de255cd832",
                Name = "Ahmet",
                Surname = "Tümkaya",
                Firm = "Test AŞ."
            };

            _personServices.Setup(x => x.UpdateAsync(personUpdate).Result).Returns(Response<NoContent>.Success(StatusCodes.Status204NoContent));
            var personController = new PersonController(_personServices.Object);
            IActionResult response = personController.Update(personUpdate).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<NoContent>;

            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode); 
        }

        [Test]
        public void Person_Delete()
        {
            string personId = "64653482503e72de255cd832"; 
            _personServices.Setup(x => x.DeleteAsync(personId).Result).Returns(Response<NoContent>.Success(StatusCodes.Status204NoContent));
            var personController = new PersonController(_personServices.Object);
            IActionResult response = personController.Delete(personId).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<NoContent>;

            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
        }

        private Response<List<PersonDto>> GetPersons()
        {

            return Response<List<PersonDto>>.Success(new List<PersonDto>()
             {
                 new PersonDto
                    {
                        CreateDate = DateTime.Now,
                        Id = "64653482503e72de255cd832",
                        Name="Ahmet",
                        Surname="Tümkaya",
                        Firm ="Test AŞ.",
                        ContactInformation = new List<ContactInformationDto> {
                 new ContactInformationDto()
                    {
                        Id = "64653482503e72de255cd832",
                        CreateDate =DateTime.Now,
                        Email ="a@c.com",
                        InformationContent="Phone",
                        Location ="Antalya",
                        ModifyDate =DateTime.Now,
                        PhoneNumber="0545411111111"

                    },
                },

            },
                new PersonDto
            {
                CreateDate = DateTime.Now,
                Id = "64653482503e72de255cd834",
                Name="Ali",
                Surname="Veli",
                Firm ="Test2 AŞ.",
                ContactInformation = new List<ContactInformationDto> {
            new ContactInformationDto()
            {
                Id = "64653482503e72de255cd835",
                CreateDate =DateTime.Now,
                Email ="aa@ca.com",
                InformationContent="Phone",
                Location ="İzmir",
                ModifyDate =DateTime.Now,
                PhoneNumber="02325555555"

            },

            },

            }
             }, 200

             );
        }

        private Response<PersonDto> CreatePerson()
        {

            return Response<PersonDto>.Success(new PersonDto()
            {
                Id = "",
                CreateDate = DateTime.Now,
                Firm = "ABC LTD. ŞTİ.",
                Name = "Ali",
                Surname = "Veli"

            }, 200); 
        }

        private Response<PersonDto> GetById()
        {
            return Response<PersonDto>.Success(new PersonDto() {
                CreateDate = DateTime.Now,
                Id = "64653482503e72de255cd832",
                Name = "Ahmet",
                Surname = "Tümkaya",
                Firm = "Test AŞ.",
                ContactInformation = new List<ContactInformationDto> {
                 new ContactInformationDto()
                    {
                        Id = "64653482503e72de255cd832",
                        CreateDate =DateTime.Now,
                        Email ="a@c.com",
                        InformationContent="Phone",
                        Location ="Antalya",
                        ModifyDate =DateTime.Now,
                        PhoneNumber="0545411111111"

                    },
                },
            },200);
        }        
    }
}
