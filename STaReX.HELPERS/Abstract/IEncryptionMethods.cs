using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.HELPERS.Abstract
{
    public interface IEncryptionMethods
    {
        string Encryption(string guid, string password);
        string Decryption(string token, string password);
    }
}
