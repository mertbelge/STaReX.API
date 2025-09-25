using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Dto;

namespace STaReX.BUSINESS.Abstract.IQCDELogReaderService
{
    public interface IQCDELogReaderService
    {
        Task<StatusResponse<NoData>> Insert();
    }
}
