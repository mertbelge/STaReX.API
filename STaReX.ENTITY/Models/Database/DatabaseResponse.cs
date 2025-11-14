using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.Database
{
    public class DatabaseResponse : IEntity
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
