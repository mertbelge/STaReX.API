using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.HELPERS.Abstract
{
    public interface IFileMethods
    {
        string GetFileNameFromFolder(string[] files);
        Task<string> GetContext(string path);
        Task<string> GetContextReplacedByCopy(string path_copy, string path_original);
    }
}
