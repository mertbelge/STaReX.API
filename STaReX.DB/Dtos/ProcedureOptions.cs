using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.DB.Dtos
{
    public class ProcedureOptions
    {
        public ConnectionTestingProcedure ConnectionTestingProcedure { get; set; } = null!;
        public HelpersAPIProcedure HelpersAPIProcedure { get; set; } = null!;
        public QCDELogProcedure QCDELogProcedure { get; set; } = null!;
    }

    public class ConnectionTestingProcedure
    {
        public string CONNECTION_TESTING { get; set; } = null!;
    }

    public class HelpersAPIProcedure
    {
        public string GET_BY_KEYWORD { get; set; } = null!;
    }

    public class QCDELogProcedure
    {
        public string INSERT { get; set; } = null!;
    }
}
