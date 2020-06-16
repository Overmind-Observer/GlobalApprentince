using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Util
{
    public interface ICustomAuthManager
    {
        string Authenticate(string username, string role);
        IDictionary<string, Tuple<string, string>> Tokens { get; }
    }
}
