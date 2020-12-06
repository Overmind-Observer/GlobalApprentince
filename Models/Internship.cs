using Global_Intern.Models.EmployerModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public string InternshipPaid { get; set; }

        // IntershipEmail- to nortify the employer. or inquiry for student
        public string InternshipEmail { get; set; } // For More Queries -- 
        public System.DateTime InternshipExpDate { get; set; }
        public System.DateTime InternshipCreatedAt { get; set; }
        public System.DateTime InternshipUpdatedAt { get; set; }

        [Required]
        [JsonIgnore]
        public virtual User User { get; set; } // Poster


        public void SetAddorUpdateIntern(InternshipModel intern, User user, bool isUpdate = false, int InternshipID = 0)
        {
            this.InternshipTitle = intern.InternshipTitle;
            this.InternshipType = intern.InternshipType;
            this.InternshipDuration = intern.InternshipDuration;
            this.InternshipBody = intern.InternshipBody;
            this.InternshipVirtual = intern.InternshipVirtual;
            this.InternshipPaid = intern.InternshipPaid;
            this.InternshipEmail = intern.InternshipEmail;
            this.InternshipExpDate = intern.InternshipExpDate;
            if (isUpdate && InternshipID != 0)
            {
                this.InternshipId = InternshipID;
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
