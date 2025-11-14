using STaReX.ENTITY.Models.ConnectionTesting;
using STaReX.ENTITY.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Models.Login;
using STaReX.ENTITY.Models.Database;

namespace STaReX.BUSINESS.Abstract.IConnectionTestingService
{
    public interface IConnectionService
    {
        Task <StatusResponse<ConnectionTesting>> GetBy();
        Task <StatusResponse<TokenResponse>>Login(LoginCredentials loginCredentials);
        Task <DatabaseResponse> Success(string _Token);
    }
}
