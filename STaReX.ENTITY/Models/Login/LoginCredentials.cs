using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.Login
{
    public class LoginCredentials: IEntity
    {
        public string usernameOrEmail { get; set; }
        public string password { get; set; }
    }
}
