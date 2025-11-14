using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.ENTITY.Models.Login;

namespace STaReX.API.Controllers.HomeController
{
    [Route("api/[controller]")]
    [ApiController]

    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        
        public ConnectionController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpGet("connection-testing")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBy()
        {

            var response = await _connectionService.GetBy();
            return Ok(response);

        }

        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
        
            var response = await _connectionService.Login(loginCredentials);
            return Ok(response);
        }


    }
}
