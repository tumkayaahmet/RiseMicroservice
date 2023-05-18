using Shared.Dtos;

namespace Report.Services
{
    public interface IReportDetailServices
    {
        Task<Response<List<ReportDetailDto>>> GetAllAsync();
        Task<Response<List<ReportDetailDto>>> GetAllDetailByIdAsync(string id);
        Task<Response<ReportDetailDto>> CreateAsync(ReportDetailCreateDto reportDetailCreateDto);
        Task<Response<NoContent>> UpdateAsync(ReportDetailUpdateDto reportDetailUpdateDto);


    }
}
