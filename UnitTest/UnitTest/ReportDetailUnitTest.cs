using Contact.Controllers;
using Contact.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Report.Controllers;
using Report.Models;
using Report.Services;
using Shared.Dtos;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ReportDetailUnitTest
    {
        private Mock<IReportDetailServices> _reportDetailServices;

        public ReportDetailUnitTest()
        {
            _reportDetailServices = new Mock<IReportDetailServices>();
        }

        [Test]
        public void Report_ReportDetail_Create()
        {
            ReportDetailCreateDto reportDetailCreateDto = new()
            {
                CreateDate = DateTime.Now,
                ReportStatus = ReportStatus.Preparing
            };
            var newReportDetail = _reportDetailServices.Setup(x => x.CreateAsync(reportDetailCreateDto).Result).Returns(CreateReportDetail());
            var reportDetailContoller = new ReportDetailController(_reportDetailServices.Object);
            IActionResult response = reportDetailContoller.Create(reportDetailCreateDto).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<ReportDetailDto>;
            Assert.IsTrue(result.Data.ReportStatus == CreateReportDetail().Data.ReportStatus);

        }

        [Test]
        public void Report_ReportDetail_GetAll()
        {
            var list = _reportDetailServices.Setup(x => x.GetAllAsync().Result).Returns(GetAllReportDeatil);
            var reportDetailContoller = new ReportDetailController(_reportDetailServices.Object);
            IActionResult response = reportDetailContoller.GetAll().GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<List<ReportDetailDto>>;

            Assert.AreEqual(GetAllReportDeatil().Data.Count, result.Data.Count);

        }

        [Test]
        public void Report_ReportDetail_GetAllDetailById()
        {
            string reportDetailId = "6465ebc58961bf8e71dbde6c";
            var getReportDetail = _reportDetailServices.Setup(x => x.GetAllDetailByIdAsync(reportDetailId).Result).Returns(GetAllReportDeatil);
            var personContoller = new ReportDetailController(_reportDetailServices.Object);
            IActionResult response = personContoller.GetById(reportDetailId).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<List<ReportDetailDto>>;

            Assert.AreEqual(GetAllReportDeatil().Data.Count, result.Data.Count);
        }

        [Test]
        public void Report_ReportDetail_Update()
        {
            ReportDetailUpdateDto reportDetailUpdateDto = new()
            {
                Id = "6465ebc58961bf8e71dbde6c",
                ReportDate = DateTime.Now,
                ReportPath = "C:\\Users\\PC\\source\\repos\\RiseMicroservice\\Report\\Report\\ReportFilesContactListReport_22052023111826.xlsx",
                ReportStatus = ReportStatus.Completed,
             };
            ReportDetailDto reportDetailDto = new() 
            {
                Id = "6465ebc58961bf8e71dbde6c",
                ReportDate = DateTime.Now,
                ReportPath = "",
                ReportStatus = ReportStatus.Preparing,
            };

            _reportDetailServices.Setup(x => x.UpdateAsync(reportDetailUpdateDto).Result).Returns(Response<NoContent>.Success(StatusCodes.Status204NoContent));
            var reportDetailController = new ReportDetailController(_reportDetailServices.Object);
            IActionResult response = reportDetailController.Update(reportDetailUpdateDto).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Response<NoContent>;

            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);


            _reportDetailServices.Setup(x => x.UpdateAsync(reportDetailUpdateDto));
            Assert.IsNotNull(_reportDetailServices, UnitTestMessages.Success);
        }
         
        private Response<ReportDetailDto> CreateReportDetail()
        {
            return Response<ReportDetailDto>.Success(new ReportDetailDto()
            {
                CreateDate = DateTime.Now,
                ReportStatus = ReportStatus.Preparing
            }, ResponseMessages.ReportDetailPreparing, 200);

        }

        private Response<List<ReportDetailDto>> GetAllReportDeatil()
        {
            return Response<List<ReportDetailDto>>.Success(new List<ReportDetailDto>()
            {
                new ReportDetailDto()
                {
                    Id = "6465ebc58961bf8e71dbde6c",
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    ReportDate = DateTime.Now,
                    ReportPath= "C:/FilePath",
                    ReportStatus = ReportStatus.Completed
                },
                new ReportDetailDto()
                {
                    Id = "6465f1c08961bf8e71dbde6d",
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    ReportDate = DateTime.Now,
                    ReportPath= "C:/FilePath",
                    ReportStatus = ReportStatus.Preparing
                },
            }, 200); 
        }
    }
}
