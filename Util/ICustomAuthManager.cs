using System;
using System.Collections.Generic;

namespace Global_Intern.Util
{
    public interface ICustomAuthManager
    {
        string Authenticate(string username, string role);
        IDictionary<string, Tuple<string, string>> Tokens { get; }
    }
}
