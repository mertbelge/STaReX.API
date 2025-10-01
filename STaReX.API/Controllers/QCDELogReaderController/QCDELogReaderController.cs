using System.Globalization;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.ENTITY.Dto;
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
        private readonly IHelperRepository<NoData> _repository;

        public QCDELogReaderController(IQCDELogReaderService qcdeLogReaderService, IWebHostEnvironment env, IHelperRepository<NoData> repository)
        { 
            _qcdeLogReaderService = qcdeLogReaderService;
            _env = env;
            _repository = repository;
        }

        [HttpPost("log-files-insert")]

        public async Task<IActionResult> Insert()
        {
            string[] files = Directory.GetFiles(Path.Combine(_env.WebRootPath, "private", "ZandronumLog"));

            string filename = _repository.GetFileNameFromFolder(files);

            var path_original = Path.Combine(_env.WebRootPath, "private", "ZandronumLog", filename);
            var path_copy = Path.Combine(_env.WebRootPath, "private", "ZandronumLog", "Copy.log");

            string content_value = _repository.GetContextReplacedByCopy(path_copy, path_original).Result;
            string full_content = _repository.GetContext(path_original).Result;

            if (content_value.Length > 0)
            {
                string[] content = content_value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                var response = await _qcdeLogReaderService.Insert(content);

                System.IO.File.WriteAllText(path_copy, full_content);

                return Ok(response);            
            }

            else
            {
                return Ok(StatusResponse<NoData>.Success());
            }
        }
    }
}
