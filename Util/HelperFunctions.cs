using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Util
{
    public class HelpersFunctions
    {
        public static T GetCSharpObject<T>(string JsonString)
        {
            JsonString = "[" + JsonString + "]";
            
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonString);
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonString);//Code to create instance
            
            return t;
        }

    }
}
