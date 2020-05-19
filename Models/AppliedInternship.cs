using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class AppliedInternship
    {
        public int AppliedInternshipId { get; set; }
        public string EmployerStatus { get; set; }
        public Student Student { get; set; }
        public Internship Internship { get; set; }
    }
}
