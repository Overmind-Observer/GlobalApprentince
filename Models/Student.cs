using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentGender { get; set; }
        public string StudentPassword { get; set; }
        public string StudentHomeCountry { get; set; }
        public string StudentCurrentCountry { get; set; }
        public string StudentVisaStatus { get; set; }
        public string StudentEducationArea { get; set; }
        public string StudentFieldInterest { get; set; }
        public string StudentLinks { get; set; } // LinkedIn, Githut, Portfolio or SkypeID
        public string StudentCV { get; set; } // filename use unique number with timestamp + .pdf or .docx
        public string StudentCoverLetter { get; set; }
        public DateTime StudentCreatedAt { get; set; }
        public DateTime StudentUpdatedAt { get; set; }
        public List<AppliedInternship> AppliedInternships { get; set; }
        public List<InternStudent> InternStudents { get; set; }
        public List<Qualification> Qualifications { get; set; }
    }
}
