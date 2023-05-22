using Contact.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : CustomBaseController
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocationStatisticsReport(string reportDetailId)
        {
            var response = await _reportServices.GetLocationStatisticsReportAsync(reportDetailId);
            return CreateActionResultInstance(response);
        }

    }
}
