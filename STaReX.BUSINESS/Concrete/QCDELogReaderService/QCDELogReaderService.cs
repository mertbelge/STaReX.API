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

        public async Task<StatusResponse<NoData>> Insert()
        {
            var procedure = _procedureOptions.QCDELogProcedure.INSERT;
            DynamicParameters parameters = new DynamicParameters();
            var result = await _repository.InsertAsync(procedure, parameters);

            if (result.Success == true)
            {
                return StatusResponse<NoData>.Success();
            }

            else
            {
                return StatusResponse<NoData>.Fail(result.Message);
            }
        }
    }
}
