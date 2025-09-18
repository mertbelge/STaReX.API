using Microsoft.AspNetCore.Mvc;
using STaReX.BUSINESS.Abstract.IWeatherService;

namespace STaReX.API.Controllers.WeatherController
{

    [Route("api/[controller]")]
    [ApiController]

    public class WeatherController : ControllerBase
    {

        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("get-weather")]
        public async Task<IActionResult> GetBy(double latitude, double longitude)
        {

            var response = await _weatherService.GetBy(latitude, longitude);
            return Ok(response);

        }
    }
}
