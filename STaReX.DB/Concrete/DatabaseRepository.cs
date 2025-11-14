using Dapper;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
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

        public async Task<IEnumerable<T?>> GetAllAsync(string query, DynamicParameters? parametes)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var result = await connection.QueryAsync<T>(query, parametes, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<T?> InsertAsync(string query, DynamicParameters? parameters)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<T?> UpdateMultipleAsync(string query, DynamicParameters? parameters)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var multipleResult = await connection.QueryMultipleAsync(query, parameters, commandType: System.Data.CommandType.StoredProcedure);

                var responseResult = await multipleResult.ReadFirstAsync<DatabaseResponse>();

                if (responseResult.Success == true)
                {
                    var result = await multipleResult.ReadFirstAsync<T>();
                    return result;
                }

                else { 

                    throw new ExceptionResponse(HttpStatusCode.InternalServerError, responseResult.Message);
                
                }

            }
        }
    }
}
