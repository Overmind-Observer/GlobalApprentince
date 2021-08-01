using Global_Intern.Data;
using Global_Intern.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Global_Intern.Util
{
    public class HelpersFunctions
    {
        IWebHostEnvironment _env;

        public HelpersFunctions(IWebHostEnvironment env)
        {
            _env = env;
        }
        public HttpResponse res;
        public static T GetCSharpObject<T>(string JsonString)
        {
            JsonString = "[" + JsonString + "]";

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonString);
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonString);//Code to create instance

            return t;
        }
        // GetMenuOptionsForUser -> return an dynamic object
        // PATH ->> GlobalApprentince\Data\DashboardMenuOptions.json
        // below mehod access the file and create a dynamic object
        public static dynamic GetMenuOptionsForUser(int user_id, string path)
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

        public void ListOfPrimeNumbers()
        {
            ConsoleLogs consoleLogs = new ConsoleLogs(_env);

            PrimeNumberGenerator primeNumberGenerator = new PrimeNumberGenerator(_env);

            Random random1 = new Random();

            Random random2 = new Random();

            int number1 = random2.Next(100);

            int number2 = random1.Next(100);

            IEnumerable<int> listOfPrimeNumbers = primeNumberGenerator.GetPrimeNumbers(number1, number2);

            foreach (int num in listOfPrimeNumbers)
            {
                consoleLogs.WriteDebugLog(num.ToString());
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

        public static string StoreFile(string path, IFormFile file)
        {
            // This method can store any type of file.
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = path + uniqueFileName;
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }

    }
}
