using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class AppliedInternship
    {
        public int AppliedInternshipId { get; set; }
        public string EmployerStatus { get; set; }
        public User User { get; set; } // Who Applied
        public Internship Internship { get; set; } // which Intership user applied
    }
}
