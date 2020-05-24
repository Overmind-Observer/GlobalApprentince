using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserHomeCountry { get; set; }
        public string UserCurrentCountry { get; set; }
        public string UserPassword { get; set; }
        public int UserPhone { get; set; }
        public string UserLinks { get; set; }
        public string UserGender { get; set; }
        public Role Role { get; set; } //FK
        public List<Qualification> qualifications { get; set; }
        public List<Experience> experiences { get; set; }
        public List<VisaStatus> visaStatuses { get; set; } // should get one row
        public List<UserCompany> userCompanies { get; set; } // should get one row
        public List<Profile> profiles { get; set; } // should get one row
        public List<InternStudent> internStudents { get; set; } // list of students who are working in some internships
        public List<AppliedInternship> appliedInternships { get; set; } // list of user applyed for intership
    }
}
