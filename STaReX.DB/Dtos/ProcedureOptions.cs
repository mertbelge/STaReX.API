using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.DB.Dtos
{
    public class ProcedureOptions
    {
        public ConnectionProcedure ConnectionProcedure { get; set; } = null!;
        public HelpersAPIProcedure HelpersAPIProcedure { get; set; } = null!;
        public QCDELogProcedure QCDELogProcedure { get; set; } = null!;
    }

    public class ConnectionProcedure
    {
        public string CONNECTION_TESTING { get; set; } = null!;
        public string LOGIN { get; set; } = null!;
        public string AUTH_CHECK { get; set; } = null!;
    }

    public class HelpersAPIProcedure
    {
        public string GET_BY_KEYWORD { get; set; } = null!;
    }

    public class QCDELogProcedure
    {
        public string INSERT { get; set; } = null!;
        public string GET_LAST_MAP { get; set; } = null!;
        public string GET_PLAYERS_LIST { get; set; } = null!;
        public string GET_CMD_COMMAND { get; set; } = null!;
    }
}
