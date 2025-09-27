using Microsoft.AspNetCore.Mvc;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;

namespace STaReX.API.Controllers.QCDELogReaderController
{

    [Route("api/[controller]")]
    [ApiController]

    public class QCDELogReaderController : ControllerBase
    {

        private readonly IQCDELogReaderService _qcdeLogReaderService;
        private readonly IWebHostEnvironment _env;

        public QCDELogReaderController(IQCDELogReaderService qcdeLogReaderService, IWebHostEnvironment env)
        { 
            _qcdeLogReaderService = qcdeLogReaderService;
            _env = env;
        }

        [HttpGet("log-files-inset")]

        public async Task<IActionResult> Insert()
        {
            var response = await _qcdeLogReaderService.Insert();
            return Ok(response);
        }
    }
}
