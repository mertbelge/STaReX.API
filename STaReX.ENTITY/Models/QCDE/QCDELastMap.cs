using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.QCDE
{
    public class QCDELastMap: IEntity
    {
        public string MapName { get; set; }
        public string ServerStatus { get; set; }
    }
}
