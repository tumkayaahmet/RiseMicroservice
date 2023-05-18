using AutoMapper;
using Report.Models;
using Shared.Dtos;

namespace Report.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<ReportDetail, ReportDetailDto>().ReverseMap();
            CreateMap<ReportDetail, ReportDetailCreateDto>().ReverseMap();
            CreateMap<ReportDetail, ReportDetailUpdateDto>().ReverseMap();
            CreateMap<Models.Report, ReportDto>().ReverseMap();
        }
    }
}
