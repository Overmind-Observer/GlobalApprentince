using Global_Intern.Models.AdditonalModels;
using System;

namespace Global_Intern.Models
{
    public class AppliedInternship
    {
        public AppliedInternship()
        {

        }
        public AppliedInternship(User user, Internship intern)
        {
            User = user;
            Internship = intern;
            ExpDate = intern.InternshipExpDate.AddDays(7); // AppliedIntern Expire in 7 days after Internship expired
        }
        public int AppliedInternshipId { get; set; }
        public string EmployerStatus { get; set; }
        public string TempCVPath { get; set; } // 
        public string TempCLPath { get; set; }
        public string DocOnePath { get; set; }
        public string DocTwoPath { get; set; }
        public string DocThreePath { get; set; }
        public string CoverLetterText { get; set; }
        public DateTime ExpDate { get; set; } // Automatic
        public bool SoftDelete { get; set; }
        public InternshipApplicationStatus ApplicationStatus { get; set; }
        public bool Shortlist { get; set; }
        public virtual User User { get; set; } // Who Applied
        public virtual Internship Internship { get; set; } // which Intership user applied
    }
}
