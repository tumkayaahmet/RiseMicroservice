using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.Services;
using Shared.ControllerBases;

namespace Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportPreparingController : CustomBaseController
    {
        private readonly IReportPreparingServices _reportPreparingServices;

        public ReportPreparingController(IReportPreparingServices reportPreparingServices)
        {
            _reportPreparingServices = reportPreparingServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reportPreparingServices.GetAllReportAsync();
            return CreateActionResultInstance(response);
        }
    }
}
