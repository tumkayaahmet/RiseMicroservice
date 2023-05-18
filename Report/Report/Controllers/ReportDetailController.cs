using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.Services;
using Shared.ControllerBases;
using Shared.Dtos;

namespace Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportDetailController : CustomBaseController
    {
        private readonly IReportDetailServices _reportDetailServices;

        public ReportDetailController(IReportDetailServices reportDetailServices)
        {
            _reportDetailServices = reportDetailServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reportDetailServices.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _reportDetailServices.GetAllDetailByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReportDetailCreateDto reportDetailCreateDto)
        {
            var response = await _reportDetailServices.CreateAsync(reportDetailCreateDto);

            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ReportDetailUpdateDto reportDetailUpdateDto)
        {
            var response = await _reportDetailServices.UpdateAsync(reportDetailUpdateDto);
            return CreateActionResultInstance(response);
        }
    }
}
