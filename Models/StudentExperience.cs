using Publisher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class StudentExperience
    {
        public int ExperienceId { get; set; }
        public string ExperienceTitle { get; set; }
        public string ExperienceType { get; set; }
        public string ExperienceCompany { get; set; }
        public string Location { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Boolean Present { get; set; }
        public Student Student { get; set; }
    }
}
