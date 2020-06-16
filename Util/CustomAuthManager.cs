using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Util
{
    public class CustomAuthManager : ICustomAuthManager
    {


        private readonly IDictionary<string, Tuple<string, string>> tokens = new Dictionary<string, Tuple<string, string>>();
        public IDictionary<string, Tuple<string, string>> Tokens => tokens;

        public string Authenticate(string username, string role)
        {
            var token = Guid.NewGuid().ToString();

            tokens.Add(token, new Tuple<string, string>(username, role));

            return token;
        }
    }
}
