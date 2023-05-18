using AutoMapper;
using MongoDB.Driver;
using Report.Models;
using Report.Settings;
using Shared.Dtos;
using Shared.Messages;

namespace Report.Services
{
    public class ReportPreparingServices : IReportPreparingServices
    {
        private readonly IReportDetailServices _reportDetailServices;
        private readonly IMongoCollection<ReportDetail> _reportDetailMongoCollection;
        private readonly IMapper _mapper;

        public ReportPreparingServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reportDetailMongoCollection = database.GetCollection<ReportDetail>(databaseSettings.ReportCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<ReportDetailDto>> GetAllReportAsync()
        {
            var reportDetail = new ReportDetailCreateDto();
            reportDetail.ReportDate = DateTime.Now;
            reportDetail.CreateDate = DateTime.Now;
            reportDetail.ReportStatus = ReportStatus.Preparing;
 

            var newReportDetail = _mapper.Map<ReportDetail>(reportDetail);

            await _reportDetailMongoCollection.InsertOneAsync(newReportDetail);
            return Response<ReportDetailDto>.Success(_mapper.Map<ReportDetailDto>(newReportDetail), ResponseMessages.ReportDetailPreparing, 200);
        }
    }
}
