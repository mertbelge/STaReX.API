using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.ConnectionTesting;
using STaReX.ENTITY.Models.Database;
using STaReX.ENTITY.Models.Login;
using STaReX.HELPERS.Abstract;
using STaReX.HELPERS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Concrete.ConnectionTestingService
{
    public class ConnectionService: IConnectionService
    {
        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<ConnectionTesting> _repositoryConnection;
        private readonly IDatabaseRepository<TokenResponse> _repositoryToken;
        private readonly IDatabaseRepository<DatabaseResponse> _repositoryCheck;
        private readonly IHelperRepository<NoData> _helperRepository;
        private readonly HelperOptions _helperOptions;

        public ConnectionService(IDatabaseRepository<ConnectionTesting> repositoryConnection,
            IDatabaseRepository<TokenResponse> repositoryToken,
            IDatabaseRepository<DatabaseResponse> repositoryCheck,
            IHelperRepository<NoData> helperRepository,
            IOptions<ProcedureOptions> procedureOptions,
            IOptions<HelperOptions> helperOptions)
        {

            _repositoryConnection = repositoryConnection;
            _repositoryToken = repositoryToken;
            _repositoryCheck = repositoryCheck;
            _helperRepository = helperRepository;
            _procedureOptions = procedureOptions.Value;
            _helperOptions = helperOptions.Value;

        }

        public async Task<StatusResponse<ConnectionTesting>> GetBy()
        {
            var procedure = _procedureOptions.ConnectionProcedure.CONNECTION_TESTING;
            var result = await _repositoryConnection.GetByIdAsync(procedure, null);
            return StatusResponse<ConnectionTesting>.Success(result);
        }

        public async Task<StatusResponse<TokenResponse>> Login(LoginCredentials loginCredentials)
        {
            var procedure = _procedureOptions.ConnectionProcedure.LOGIN;
            var key = _helperOptions.EncryptionOptions.KEY;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UsernameOrEmail", loginCredentials.usernameOrEmail);
            parameters.Add("Password", loginCredentials.password);

            var result = await _repositoryToken.UpdateMultipleAsync(procedure, parameters);
            result.Token = _helperRepository.Encryption(result.Token, key);

            return StatusResponse<TokenResponse>.Success(result);

        }

        public async Task<DatabaseResponse> Success(string _Token)
        {
            var procedure = _procedureOptions.ConnectionProcedure.AUTH_CHECK;
            var key = _helperOptions.EncryptionOptions.KEY;
            _Token = _helperRepository.Decryption(_Token, key);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Token", _Token);

            var result = await _repositoryCheck.GetByIdAsync(procedure, parameters);

            return result;
        }
    }
}
