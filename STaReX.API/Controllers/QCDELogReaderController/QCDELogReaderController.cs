using System.Globalization;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using STaReX.API.Filters;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Database;
using STaReX.ENTITY.Models.Holidays;
using STaReX.HELPERS.Abstract;

namespace STaReX.API.Controllers.QCDELogReaderController
{

    [Route("api/[controller]")]
    [ApiController]

    public class QCDELogReaderController : ControllerBase
    {

        private readonly IQCDELogReaderService _qcdeLogReaderService;
        private readonly IWebHostEnvironment _env;
        private readonly IFileMethods _fileMethods;

        public QCDELogReaderController(IQCDELogReaderService qcdeLogReaderService, IWebHostEnvironment env, IFileMethods fileMethods)
        { 
            _qcdeLogReaderService = qcdeLogReaderService;
            _env = env;
            _fileMethods = fileMethods;
        }

        [HttpPost("log-files-insert")]
        [ServiceFilter(typeof(DBFilter))]
        public async Task<IActionResult> Insert(AgentKeyword agentKeyword)
        {
            string[] files = Directory.GetFiles(Path.Combine(_env.WebRootPath, "private", "ZandronumLog"));

            string filename = _fileMethods.GetFileNameFromFolder(files);

            var path_original = Path.Combine(_env.WebRootPath, "private", "ZandronumLog", filename);
            var path_copy = Path.Combine(_env.WebRootPath, "private", "ZandronumLog", "Copy.log");

            string content_value = _fileMethods.GetContextReplacedByCopy(path_copy, path_original).Result;
            string full_content = _fileMethods.GetContext(path_original).Result;

            if (content_value.Length > 0)
            {
                string[] content = content_value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                var response = await _qcdeLogReaderService.Insert(agentKeyword.agentKey, content);

                System.IO.File.WriteAllText(path_copy, full_content);

                return Ok(response);            
            }

            else
            {
                return Ok(StatusResponse<NoData>.Success());
            }
        }

        [HttpGet("get-qcde-server-info-list")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetList()
        {

            var response = await _qcdeLogReaderService.GetList();
            return Ok(response);
        
        }

        [HttpPost("qcde-server-launch")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> StartUp()
        {

            var response = await _qcdeLogReaderService.StartUp();
            return Ok(response);

        }
    }
}
