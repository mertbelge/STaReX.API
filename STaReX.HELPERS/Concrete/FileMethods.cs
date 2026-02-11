using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using STaReX.HELPERS.Abstract;

namespace STaReX.HELPERS.Concrete
{
    public class FileMethods: IFileMethods
    {

        public async Task<string> GetContext(string path)
        {
            string content;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs))
            {
                content = await reader.ReadToEndAsync();
            }

            return content;

        }

        public async Task<string> GetContextReplacedByCopy(string path_copy, string path_original)
        {
            string content = await GetContext(path_original);

            string content_copy = System.IO.File.ReadAllText(path_copy);

            if (content_copy.Length > 0)
            {
                content = content.Replace(content_copy, "");
            }

            return content;

        }

        public string GetFileNameFromFolder(string[] files)
        {
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

            return apply_filename;
        }
    }
}
