using Dapper;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.HELPERS.Abstract
{
    public interface IHelperRepository <T> where T : IEntity
    {
        Task<T?> GetAllAsync(string keyword, DynamicParameters parameters);
        Task<HelpersAPI> GetURL(string keyword);
    }
}
