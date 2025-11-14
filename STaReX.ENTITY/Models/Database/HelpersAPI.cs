using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Database
{
    public class HelpersAPI : IEntity
    {
        public string URL { get; set; }
        public string APPKey1 { get; set; }
        public string APPKey2 { get; set; }
    }
}
