using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.Login
{
    public class TokenResponse : IEntity
    {
        public string Token { get; set; }
    }
}
