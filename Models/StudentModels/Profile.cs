using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.StudentModels
{
    public class InternStudent
    {
        public string StudentType { get; set; } //Domestic, International
        public string StudentWorkingRight { get; set; } // yes, no
        public string StudentVisaType { get; set; }  //Student visa, Open-work visa, others
        public string StudentVisaExpire { get; set; } //2020/12
        public string StudentIndustryCertificates { get; set; } //CCNA 1
        public string StudentDriverType { get; set; } //Learner
        public string StudentDob { get; set; } //1994
        public string StudentEthnic { get; set; } //Asian

    }
}
