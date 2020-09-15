using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.StudentModels
{
    public class InternStudent
    {
        public string UserStudentType { get; set; } //Domestic, International
        public string UserWorkingRight { get; set; } // yes, no
        public string UserVisaType { get; set; }  //Student visa, Open-work visa, others
        public string UserVisaExpire { get; set; } //2020/12
        public string UserIndustryCertificates { get; set; } //CCNA 1
        public string UserDriverType { get; set; } //Learner
        public string UserDob { get; set; } //1994
        public string UserEthnic { get; set; } //Asian

    }
}
