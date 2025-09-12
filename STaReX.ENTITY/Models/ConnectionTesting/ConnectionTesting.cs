using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.ConnectionTesting
{
    public class ConnectionTesting : IEntity
    {
        public string? Result { get; set; } = null!;
    }
}
