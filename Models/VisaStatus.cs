using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models
{
    public class VisaStatus
    {
        public int      VisaStatusId { get; set; }
        public string   VisaType { get; set; }
        public int      VisaNumber { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
