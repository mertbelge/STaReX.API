using Dapper;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.HELPERS.Abstract
{
    public interface IHelperRepository <T> where T : IEntity
    {
        Task<T?> GetAllAsyncFromAPI(string keyword, DynamicParameters parameters);
        Task<HelpersAPI> GetURL(string keyword);
        string GetFileNameFromFolder(string[] files);
        Task<string> GetContext(string path);
        Task<string> GetContextReplacedByCopy(string path_copy, string path_original);
        string Encryption(string guid, string password);
        string Decryption(string token, string password);
    }
}
