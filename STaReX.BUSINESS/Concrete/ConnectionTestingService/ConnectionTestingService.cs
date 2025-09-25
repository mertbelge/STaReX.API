using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;
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
        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<ConnectionTesting> _repository;

        public ConnectionTestingService(IDatabaseRepository<ConnectionTesting> repository, IOptions<ProcedureOptions> options)
        {

            _repository = repository;
            _procedureOptions = options.Value;

        }

        public async Task<StatusResponse<ConnectionTesting>> GetBy()
        {
            var procedure = _procedureOptions.ConnectionTestingProcedure.CONNECTION_TESTING;
            var result = await _repository.GetByIdAsync(procedure, null);
            return StatusResponse<ConnectionTesting>.Success(result);
        }

    }
}
