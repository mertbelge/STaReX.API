using Dapper;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.QCDE;

namespace STaReX.BUSINESS.Concrete.QCDELogReaderService
{
    public class QCDELogReaderService : IQCDELogReaderService
    {
        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<DatabaseResponse> _repositoryInsert;
        private readonly IDatabaseRepository<QCDELastMap> _repositoryGetLastMap;
        private readonly IDatabaseRepository<QCDEPlayer> _repositoryGetPlayersList;
        public QCDELogReaderService(IDatabaseRepository<QCDEPlayer> repositoryGetPlayersList, IDatabaseRepository<QCDELastMap> repositoryGetLastMap, IDatabaseRepository<DatabaseResponse> repositoryInsert, IOptions<ProcedureOptions> options)
        {
            _repositoryGetPlayersList = repositoryGetPlayersList;
            _repositoryInsert = repositoryInsert;
            _repositoryGetLastMap = repositoryGetLastMap;
            _procedureOptions = options.Value;
        }

        public async Task<StatusResponse<NoData>> Insert(string agentKey, string[] context)
        {
            int total_successes = 0;
            var procedure = _procedureOptions.QCDELogProcedure.INSERT;

            foreach (string context_item in context)
            { 

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("AgentKeyword", agentKey);
                parameters.Add("Context", context_item);
                var result = await _repositoryInsert.InsertAsync(procedure, parameters);

                if (result.Success == true) { total_successes++;  }

            }

            if (total_successes == context.Length){ return StatusResponse<NoData>.Success(); }
            else{ return StatusResponse<NoData>.Fail("Context Length And Total Successes Are Not Equal!"); }

        }

        public async Task<StatusResponse<QCDEServerInfo>> GetList()
        {
            var procedure = _procedureOptions.QCDELogProcedure.GET_LAST_MAP;
            var responseGetMap = await _repositoryGetLastMap.GetByIdAsync(procedure, null);

            procedure = _procedureOptions.QCDELogProcedure.GET_PLAYERS_LIST;
            var responesGetPlayersList = await _repositoryGetPlayersList.GetAllAsync(procedure, null);

            var response = new QCDEServerInfo
            {
                QCDELastMap = responseGetMap,
                QCDEPlayers = responesGetPlayersList
            };

            return StatusResponse<QCDEServerInfo>.Success(response);
        }
    }
}
