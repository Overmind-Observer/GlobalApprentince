using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Util
{
    //public interface ISingleTokenManager {
    //    string Authenticate(string username, string token);

    //    Tupp
    //}
    public interface ICustomAuthManager
    {
        string Authenticate(string username, string role,int id, bool IsAddToken=false);
        IDictionary<string, Tuple<string, string, int>> Tokens { get; }
    }
}
