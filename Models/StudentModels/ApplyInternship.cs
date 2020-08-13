using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.StudentModels
{
    public class ApplyInternship
    {
       public bool isCVExisiting { get; set; } // CV (Resume)
       public bool isCLExisiting { get; set; } // Cover letter
        public int UserCV { get; set; } // 
       public int UserCL { get; set; }
       public string TemperaryCoverLetter { get; set; }
    }
}
