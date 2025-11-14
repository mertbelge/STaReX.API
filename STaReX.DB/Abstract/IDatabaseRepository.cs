using Dapper;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.DB.Abstract
{
    public interface IDatabaseRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(string query, DynamicParameters? parametes);

        Task<IEnumerable<T?>> GetAllAsync(string query, DynamicParameters? parametes);
        Task<T?> InsertAsync(string query, DynamicParameters? parametes);
        Task<T?> UpdateMultipleAsync(string query, DynamicParameters? parameters);
    }
}
