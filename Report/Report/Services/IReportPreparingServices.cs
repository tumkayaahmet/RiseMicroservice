using Report.Models;
using Shared.Dtos;

namespace Report.Services
{
    public interface IReportPreparingServices
    {
        Task<Response<ReportDetailDto>> GetAllReportAsync();

    }
}
