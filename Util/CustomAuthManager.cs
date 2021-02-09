using System;
using System.Collections.Generic;
using System.Linq;

namespace Global_Intern.Util
{
    public class CustomAuthManager : ICustomAuthManager
    {


        private IDictionary<string, Tuple<string, string, int>> tokens = new Dictionary<string, Tuple<string, string, int>>();
        public IDictionary<string, Tuple<string, string, int>> Tokens => tokens;
        /// <summary>
        /// A Token can have multiple info stored. For Now I'm storing [User Name, User Role and ID]
        /// In future to can also store Data type to tell server about the token expiry date.
        /// </summary>

        public string Authenticate(string username, string role, int id, bool IsAddToken)
        {

            var token = Guid.NewGuid().ToString();
            if (!IsAddToken)
            {
                tokens = new Dictionary<string, Tuple<string, string, int>>();
            }
            tokens.Add(token, new Tuple<string, string, int>(username, role, id));

            return token;
        }

        public bool removeToken(string gUIDtokenKey)
        {
            if (tokens.FirstOrDefault().Key == gUIDtokenKey)
            {
                tokens.Remove(gUIDtokenKey);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
