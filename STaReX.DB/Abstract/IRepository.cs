using Dapper;
using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.DB.Abstract
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(string query, DynamicParameters? parametes );
    }
}
