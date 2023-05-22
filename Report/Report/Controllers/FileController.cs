using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Report.Models;
using Report.Services;
using Report.Settings;
using Shared.ControllerBases;
using Shared.Dtos;
using Shared.Messages;

namespace Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : CustomBaseController
    {
        private readonly IMongoCollection<ReportDetail> _reportDetailMongoCollection;
        private readonly IReportDetailServices _reportDetailServices;

        private readonly IMapper _mapper;

        public FileController(IMapper mapper, IDatabaseSettings databaseSettings, IReportDetailServices reportDetailServices)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reportDetailMongoCollection = database.GetCollection<ReportDetail>(databaseSettings.ReportCollectionName);
            _mapper = mapper;
            _reportDetailServices = reportDetailServices;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string reportDetailId)
        {
            try
            {
                if (file is not { Length: > 0 }) return BadRequest();
                var reportDetail = await _reportDetailMongoCollection.Find<ReportDetail>(x => x.Id == reportDetailId).ToListAsync();
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "ReportFiles");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                string date = DateTime.Now.ToString();
                string fileName = "ContactListReport_" + date.Replace(".", "").Replace(" ", "").Replace(":", "") + ".xlsx"; // İlgili dosya adını buraya ekleyin
                string filePath = Path.Combine(folderPath, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var reportDetailUpdateDto = new ReportDetailUpdateDto()
                {
                    Id = reportDetailId,
                    ReportDate = DateTime.Now,
                    ReportPath = folderPath + fileName,
                    ReportStatus = ReportStatus.Completed
                };
                var response = await _reportDetailServices.UpdateAsync(reportDetailUpdateDto);
                return CreateActionResultInstance(response);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}