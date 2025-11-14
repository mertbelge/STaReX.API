using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.QCDE
{
    public class QCDEStartUp: IEntity
    {
        public string ExecPath { get; set; }
        public string WorkingDirectory { get; set; }
        public string Arguments { get; set; }
    }
}
