using Microsoft.AspNetCore.Mvc;
using STaReX.API.Filters;
using STaReX.BUSINESS.Abstract.IHolidayService;

namespace STaReX.API.Controllers.HolidayController
{
    [Route("api/[controller]")]
    [ApiController]

    public class InformationController : ControllerBase
    {

        private readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpGet("get-holidays")]

        public async Task<IActionResult> GetList()
        {
            var response = await _informationService.GetList();
            return Ok(response);
        }

        [HttpGet("get-weather")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetBy(double latitude, double longitude)
        {

            var response = await _informationService.GetBy(latitude, longitude);
            return Ok(response);

        }
    }

}
