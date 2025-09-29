using System.Globalization;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.ENTITY.Dto;

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

        [HttpPost("log-files-insert")]

        public async Task<IActionResult> Insert()
        {

            string[] files = Directory.GetFiles(Path.Combine(_env.WebRootPath, "private", "ZandronumLog"));
            string filename;
            string apply_filename = "";
            string converted_filename;
            string date_format = "yyyy_MM_dd-HH_mm_ss";
            DateTime date = new DateTime(2024, 1, 1);
            DateTime converted_date = new DateTime();

            foreach (var file in files)
            {
            
                filename = Path.GetFileName(file);
                converted_filename = filename.Replace("Q-Zandronum__", "");
                converted_filename = converted_filename.Replace(".log", "");

                if (converted_filename != "Copy")
                {
                    converted_date = DateTime.ParseExact(converted_filename, date_format, CultureInfo.InvariantCulture);

                    if (converted_date > date)
                    {
                        apply_filename = filename;
                        date = converted_date;
                    }
                }

            }

            var path_original = Path.Combine(_env.WebRootPath, "private", "ZandronumLog", apply_filename);
            var path_copy = Path.Combine(_env.WebRootPath, "private", "ZandronumLog", "Copy.log");

            string content_original = System.IO.File.ReadAllText(path_original);
            string content_copy = System.IO.File.ReadAllText(path_copy);

            if (content_copy.Length > 0)
            { 
                content_original = content_original.Replace(content_copy, "");
            }

            if (content_original.Length > 0)
            {
                string[] content = content_original.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                var response = await _qcdeLogReaderService.Insert(content);

                System.IO.File.WriteAllText(path_copy, content_original);

                return Ok(response);            
            }

            else
            {
                return Ok(StatusResponse<NoData>.Success());
            }
        }
    }
}
