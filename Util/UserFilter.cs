using Global_Intern.Models;
using System.Collections.Generic;
using System.Linq;

namespace Global_Intern.Util
{
    public class UserFilter
    {

        public static List<Internship> RemoveUserInfoFromInternship(List<Internship> source)
        {

            for (int i = 0; i < source.Count(); i++)
            {
                source[i].User.UserPassword = "";
                source[i].User.Salt = "";
            }

            return source;
        }
    }
}
