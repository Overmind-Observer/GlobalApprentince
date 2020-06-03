using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class InternStudent
    {
        public int InternStudentId { get; set; }
        public User User { get; set; } // selected user the intership
        public Internship Internship { get; set; }
    }
}
