using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STaReX.BUSINESS.Abstract.IBlogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.API.Controllers.BlogController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController: ControllerBase
    {

        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("get-list")]
        [AllowAnonymous]

        public async Task<IActionResult> GetList()
        {
            var response = await _blogService.GetList();
            return Ok(response);
        }
    }
}
