using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.QCDE;

namespace STaReX.BUSINESS.Abstract.IQCDELogReaderService
{
    public interface IQCDELogReaderService
    {
        Task<StatusResponse<QCDEServerInfo>> GetList();
        Task<StatusResponse<NoData>> Insert(string agentKey, string[] context);
        Task<StatusResponse<NoData>> StartUp();
    }
}
