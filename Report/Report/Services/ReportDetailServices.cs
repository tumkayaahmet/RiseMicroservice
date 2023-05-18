using AutoMapper;
using MongoDB.Driver;
using Report.Models;
using Report.Settings;
using Shared.Dtos;
using Shared.Messages;

namespace Report.Services
{
    public class ReportDetailServices : IReportDetailServices
    {
        private readonly IMongoCollection<ReportDetail> _reportDetailMongoCollection;
        private readonly IMapper _mapper;

        public ReportDetailServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reportDetailMongoCollection = database.GetCollection<ReportDetail>(databaseSettings.ReportCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<ReportDetailDto>> CreateAsync(ReportDetailCreateDto reportDetailCreateDto)
        {
            var newReportDetail = _mapper.Map<ReportDetail>(reportDetailCreateDto); 

            await _reportDetailMongoCollection.InsertOneAsync(newReportDetail);
            return Response<ReportDetailDto>.Success(_mapper.Map<ReportDetailDto>(newReportDetail), ResponseMessages.ReportDetailPreparing, 200);
        }

        public async Task<Response<List<ReportDetailDto>>> GetAllAsync()
        {
            var reportDetail = await _reportDetailMongoCollection.Find(report => true).ToListAsync();
            var reportCount = reportDetail.Count();
            return Response<List<ReportDetailDto>>.Success(_mapper.Map<List<ReportDetailDto>>(reportDetail),ResponseMessages.DataCount + reportCount, 200);
        }

        public async Task<Response<List<ReportDetailDto>>> GetAllDetailByIdAsync(string id)
        {
            var reportDetailById = await _reportDetailMongoCollection.Find<ReportDetail>(x => x.Id == id).ToListAsync();
            return Response<List<ReportDetailDto>>.Success(_mapper.Map<List<ReportDetailDto>>(reportDetailById),ResponseMessages.Success, 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportDetailUpdateDto reportDetailUpdateDto)
        {
            var updateReportDetail = _mapper.Map<ReportDetail>(reportDetailUpdateDto);
            updateReportDetail.ModifyDate = DateTime.Now;
            updateReportDetail.ReportPath = "";
            updateReportDetail.ReportStatus = ReportStatus.Completed;
            var result = await _reportDetailMongoCollection.FindOneAndReplaceAsync(x => x.Id == reportDetailUpdateDto.Id, updateReportDetail);

            if (result == null)
            {
                return Response<NoContent>.Fail(ResponseMessages.ReportDetailNotFound, 404);
            }
            return Response<NoContent>.Success(ResponseMessages.ReportDetailCompleted, 204);
        }
    }
}
