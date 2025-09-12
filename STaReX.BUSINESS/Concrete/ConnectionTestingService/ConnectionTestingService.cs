using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Models.ConnectionTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Concrete.ConnectionTestingService
{
    public class ConnectionTestingService: IConnectionTestingService
    {
        private readonly IConfiguration _configuration;
        private readonly ProcedureOptions _procedureOptions;
        private readonly IRepository<ConnectionTesting> _repository;

        public ConnectionTestingService(IConfiguration configuration, IRepository<ConnectionTesting> repository, IOptions<ProcedureOptions> options)
        {

            _configuration = configuration;
            _repository = repository;
            _procedureOptions = options.Value;

        }

        public async Task<ConnectionTesting> GetBy()
        {
            var procedure = _procedureOptions.ConnectionTestingProcedure.CONNECTION_TESTING;
            var result = await _repository.GetByIdAsync(procedure, null);
            return result;
        }

    }
}
