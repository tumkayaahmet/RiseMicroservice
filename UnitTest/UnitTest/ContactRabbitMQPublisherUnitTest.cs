using Contact.Services;
using Moq;
using Shared.Dtos;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ContactRabbitMQPublisherUnitTest
    {
        private readonly Mock<IRabbitMQPublisher> _rabbitMQPublisher = new();

        [Test]
        public void Person_RabbitMQ_Publish()
        {
            List<ReportDto> reportDto = new List<ReportDto>();
            reportDto.Add(new ReportDto()
            {
                Location = "Antalya",
                PersonCount = 150,
                ReportDetailId = "6465ebc58961bf8e71dbde6c"
            });
            _rabbitMQPublisher.Setup(x => x.Publish(reportDto));
            Assert.IsNotNull(_rabbitMQPublisher, UnitTestMessages.Success);

        }
 
    }
}
