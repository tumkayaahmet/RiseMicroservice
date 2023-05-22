using Shared.Dtos;

namespace Contact.Services
{
    public interface IRabbitMQPublisher
    {
        void Publish(List<ReportDto> reportDto);
    }
}
