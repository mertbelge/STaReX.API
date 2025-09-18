using Dapper;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace STaReX.DB.Concrete
{
    public class DatabaseRepository<T> : IDatabaseRepository<T> where T : IEntity
    {

        private readonly DBContext _dbContext;

        public DatabaseRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(string query, DynamicParameters? parametes)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<T>(query, parametes, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
