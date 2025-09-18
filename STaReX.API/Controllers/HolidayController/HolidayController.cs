using Microsoft.AspNetCore.Mvc;
using STaReX.BUSINESS.Abstract.IHolidayService;

namespace STaReX.API.Controllers.HolidayController
{
    [Route("api/[controller]")]
    [ApiController]

    public class HolidayController : ControllerBase
    {

        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
             _holidayService = holidayService;
        }

        [HttpGet("get-holidays")]

        public async Task<IActionResult> GetList()
        {
            var response = await _holidayService.GetList();
            return Ok(response);
        }
    }

}
