using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
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
        public string InternshipContactEmail { get; set; } // For More Queries
        public string InternshipCloseDate { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public Employer Employer { get; set; }
        public List<AppliedInternship> AppliedInternships { get; set; }

        public List<InternStudent> InternStudents { get; set; }
    }
}
