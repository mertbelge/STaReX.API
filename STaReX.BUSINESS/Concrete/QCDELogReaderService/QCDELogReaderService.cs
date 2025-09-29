using Dapper;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;

namespace STaReX.BUSINESS.Concrete.QCDELogReaderService
{
    public class QCDELogReaderService : IQCDELogReaderService
    {
        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<DatabaseResponse> _repository;
        public QCDELogReaderService(IDatabaseRepository<DatabaseResponse> repository, IOptions<ProcedureOptions> options)
        {
            _repository = repository;
            _procedureOptions = options.Value;
        }

        public async Task<StatusResponse<NoData>> Insert(string[] context)
        {
            int total_successes = 0;
            var procedure = _procedureOptions.QCDELogProcedure.INSERT;

            for (int i = 0; i < context.Length; i++)
            { 

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Context", context[i]);
                var result = await _repository.InsertAsync(procedure, parameters);

                if (result.Success == true) { total_successes++;  }

            }

            if (total_successes == context.Length){ return StatusResponse<NoData>.Success(); }
            else{ return StatusResponse<NoData>.Fail("Context Length And Total Successes Are Not Equal!"); }

        }
    }
}
