using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Models.Database;

namespace STaReX.HELPERS.Abstract
{
    public interface IRequestMethods<T> where T : IEntity
    {
        Task<T?> GetAllAsyncFromAPI(string keyword, DynamicParameters parameters);
        Task<HelpersAPI> GetURL(string keyword);
    }
}
