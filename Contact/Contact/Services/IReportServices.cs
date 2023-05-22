using Shared.Dtos;

namespace Contact.Services
{
    public interface IReportServices
    {
        Task<Response<NoContent>> GetLocationStatisticsReportAsync(string reportDetailId);

    }
}
