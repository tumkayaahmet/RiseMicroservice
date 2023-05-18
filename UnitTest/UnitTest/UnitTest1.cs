using Contact.Controllers;
using Contact.Services;
using System.Runtime.CompilerServices;

namespace UnitTest
{
    public class Tests
    {
        private readonly IContactInformationServices _contactInformationServices;
        public Tests(IContactInformationServices contactInformationServices)
        {
            _contactInformationServices = contactInformationServices;
        }
        [SetUp]
        public async Task Setup()
        {
            var contactResult = await (new ContactInformationController(_contactInformationServices).GetAllbyPersonIdAsync("4545645"));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}