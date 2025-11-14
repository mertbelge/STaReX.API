using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.QCDE
{
    public class QCDEServerInfo: IEntity
    {
        public QCDELastMap QCDELastMap { get; set; }
        public IEnumerable<QCDEPlayer> QCDEPlayers { get; set; } 
    }
}
