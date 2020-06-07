using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Internship
    {
        public int InternshipId { get; set; }
        [Required]
        public string InternshipTitle { get; set; }
        public string InternshipType { get; set; }
        public string InternshipDuration { get; set; }
        [Required]
        public string InternshipBody { get; set; }
        public bool InternshipVirtual { get; set; }
        public bool InternshipPaid { get; set; }
        public string InternshipExperienceLevel { get; set; }

        // IntershipEmail- to nortify the employer. or inquiry for student
        public string InternshipEmail { get; set; } // For More Queries -- 
        public System.DateTime InternshipExpDate { get; set; }
        public System.DateTime InternshipCreatedAt { get; set; }
        public System.DateTime InternshipUpdatedAt { get; set; }

        [Required]
        public virtual User User { get; set; } // Poster


        public void SetAddorUpdateIntern(Internship intern, User user, bool isUpdate=false) {
            this.InternshipTitle = intern.InternshipTitle;
            this.InternshipType = intern.InternshipType;
            this.InternshipDuration = intern.InternshipDuration;
            this.InternshipBody = intern.InternshipBody;
            this.InternshipVirtual = intern.InternshipVirtual;
            this.InternshipPaid = intern.InternshipPaid;
            this.InternshipExperienceLevel = intern.InternshipExperienceLevel;
            this.InternshipEmail = intern.InternshipEmail;
            this.InternshipExpDate = intern.InternshipExpDate;
            if (isUpdate)
            {
                this.InternshipUpdatedAt = DateTime.Now;
            }
            else
            {
                this.InternshipCreatedAt = DateTime.Now;
            }
            
            this.User = user;
        }
    }
}
