using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Global_Intern.Models;
using Global_Intern.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http; 

namespace Global_Intern.Util
{
    public class HelpersFunctions
    {
        public HttpResponse res;
        public static T GetCSharpObject<T>(string JsonString)
        {
            JsonString = "[" + JsonString + "]";
            
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonString);
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonString);//Code to create instance
            
            return t;
        }
        // GetMenuOptionsForUser -> return an dynamic object
        public static dynamic GetMenuOptionsForUser(int user_id,string path)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                User user = _context.Users.Include(p => p.Role).SingleOrDefault(x => x.UserId == user_id);
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject<dynamic>(json);
                    foreach (var role in items["Roles"])
                    {
                        try
                        {
                            string currentRole = FirstLetterToUpper(user.Role.RoleName);
                            var itemObj = role[currentRole];
                            if (itemObj != null)
                            {
                                var MenuOptions = itemObj;
                                return MenuOptions;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    return null;
                }
            }
            

        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

    }
}
