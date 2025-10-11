using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.QCDE
{
    public class QCDEPlayer: IEntity
    {
        public string PlayerPlace { get; set; }
        public string PlayerName { get; set; }
        public string PlayerFrag { get; set; }
        public string PlayerDeath { get; set; }
    }
}
