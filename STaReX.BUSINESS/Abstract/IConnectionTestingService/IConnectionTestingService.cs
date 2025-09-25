using STaReX.ENTITY.Models.ConnectionTesting;
using STaReX.ENTITY.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Abstract.IConnectionTestingService
{
    public interface IConnectionTestingService
    {
        Task <StatusResponse<ConnectionTesting>> GetBy();
    }
}
