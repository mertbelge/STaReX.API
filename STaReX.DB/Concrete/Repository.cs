using Dapper;
using STaReX.DB.Abstract;
using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.DB.Concrete
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {

        private readonly DBContext _dbContext;

        public Repository(DBContext dbContext)
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
