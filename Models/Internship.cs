using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Internship
    {
        public int InternshipId { get; set; }
        public string InternshipTitle { get; set; }
        public string InternshipType { get; set; }
        public string InternshipDuration { get; set; }
        public string InternshipBody { get; set; }
        public bool InternshipVirtual { get; set; }
        public string InternshipExperienceLevel { get; set; }
        public string InternshipEmail { get; set; } // For More Queries
        public System.DateTime InternshipExpDate { get; set; }
        public System.DateTime InternshipCreatedAt { get; set; }
        public System.DateTime InternshipUpdatedAt { get; set; }
        public virtual User User { get; set; } // Poster
    }
}
