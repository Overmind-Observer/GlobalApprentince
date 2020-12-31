using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Global_Intern.Util
{
    public class ConsoleLogs
    {
        private string sLogFormat;
        private string sErrorDate;
        IWebHostEnvironment _env;
        string path;
        //This part is used to Intialize the logging process by getting timw and date

        public ConsoleLogs(IWebHostEnvironment env)
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorDate = sDay+ sMonth + sYear;
            _env = env;
        }

         

        //This method is used to write directly to the file
        public void WriteErrorLog(string sErrMsg)
        {
            path = _env.ContentRootPath + @"\Data\"+sErrorDate +"LogFile.log";
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }

    }
}
 