using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class Employer
    {
        public int EmployerId {get; set;}
        public string EmployerFirstName { get; set; }
        public string EmployerLastName { get; set; }
        public string EmployerCompany { get; set; }
        public string EmployerEmail { get; set; }
        public string EmployerPassword { get; set; }
        public string EmployerPhone { get; set; }
        public bool EmployerApproved { get; set; } // approved by Admin
        public List<Internship> Internships { get; set; } 
    }
}
