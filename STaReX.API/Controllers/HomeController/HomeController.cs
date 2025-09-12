using Microsoft.AspNetCore.Mvc;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.ENTITY.Models.ConnectionTesting;

namespace STaReX.API.Controllers.HomeController
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        private readonly IConnectionTestingService _connectionTestingService;
        
        public HomeController(IConnectionTestingService connectionTestingService)
        {
            _connectionTestingService = connectionTestingService;
        }

        [HttpGet("connection-testing")]
        public async Task<IActionResult> GetBy()
        {

            var response = await _connectionTestingService.GetBy();
            return Ok(response);

        }

    }
}
