using System;

namespace Global_Intern.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string ExperienceTitle { get; set; }
        public int ExperienceCompany { get; set; }
        public int ExperienceLocation { get; set; }
        public DateTime ExperienceStartDate { get; set; }
        public DateTime ExperienceEndDate { get; set; } // If Still working is true this field will go empty
        public bool ExperienceStillWorking { get; set; }
        public virtual User User { get; set; }
    }
}
