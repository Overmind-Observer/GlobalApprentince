using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class EmployerLike
    {
        public int EmployerLikeID { get; set; }
        public User Employer { get; set; }
        public User Intern { get; set; }
        public bool SoftDelete { get; set; }

        public EmployerLike() { }
        public EmployerLike(User employer,User intern) 
        {
            this.Employer = employer;
            this.Intern = intern;
            this.SoftDelete = false;
        }

    }

    
}
