using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string ProfilePic { get; set; } // added via unique code.
        public string ProfileCV { get; set; }
        public string ProfileCoverLetter { get; set; }
        public string ProfilePersonalStatement { get; set; }
        public string ProfileAmbitionSummnary { get; set; } // what you want to be and why you choose this field.
        public string ProfileExperience { get; set; } // what user is learning or practising at the moement
        public string ProfileRoleFit { get; set; } // what make user unique
        public string ProfileAcademicRecord { get; set; } // pdf type with all combined qualifiaction details 
        [Required]
        public virtual User User { get; set; }
    }
}
