using Dapper;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Database;
using STaReX.ENTITY.Models.QCDE;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace STaReX.BUSINESS.Concrete.QCDELogReaderService
{
    public class QCDELogReaderService : IQCDELogReaderService
    {
        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<DatabaseResponse> _repositoryInsert;
        private readonly IDatabaseRepository<QCDELastMap> _repositoryGetLastMap;
        private readonly IDatabaseRepository<QCDEPlayer> _repositoryGetPlayersList;
        private readonly IDatabaseRepository<QCDEStartUp> _repositoryStartUp;
        public QCDELogReaderService(IDatabaseRepository<QCDEStartUp> repositoryStartUp, 
            IDatabaseRepository<QCDEPlayer> repositoryGetPlayersList, 
            IDatabaseRepository<QCDELastMap> repositoryGetLastMap, 
            IDatabaseRepository<DatabaseResponse> repositoryInsert, 
            IOptions<ProcedureOptions> options)
        {
            _repositoryGetPlayersList = repositoryGetPlayersList;
            _repositoryInsert = repositoryInsert;
            _repositoryGetLastMap = repositoryGetLastMap;
            _repositoryStartUp = repositoryStartUp;
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

        public async Task<StatusResponse<NoData>> StartUp()
        {

            var procedure = _procedureOptions.QCDELogProcedure.GET_CMD_COMMAND;
            var response = await _repositoryStartUp.GetByIdAsync(procedure, null);

            var psi = new ProcessStartInfo
            {
                FileName = response.ExecPath,
                Arguments = response.Arguments,
                WorkingDirectory = response.WorkingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            using (var proc = new Process { StartInfo = psi })
            {
                var sbOut = new StringBuilder();
                var sbErr = new StringBuilder();

                proc.OutputDataReceived += (s, e) => { if (e.Data != null) sbOut.AppendLine(e.Data); };
                proc.ErrorDataReceived += (s, e) => { if (e.Data != null) sbErr.AppendLine(e.Data); };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                bool exited = proc.WaitForExit(60000);
                if (!exited)
                {
                    return StatusResponse<NoData>.Fail(sbErr.ToString());
                }
                else
                {
                    return StatusResponse<NoData>.Success();
                }
            }           
        }
    }
}
