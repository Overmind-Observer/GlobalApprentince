using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models.EmployerModels
{

    // What employer will send to add Internship
    public class InternshipModel
    {
        [Required]
        public string InternshipTitle { get; set; }
        public string InternshipType { get; set; }
        public string InternshipDuration { get; set; }
        [Required]
        public string InternshipBody { get; set; }
        public bool InternshipVirtual { get; set; }
        public string InternshipPaid { get; set; }
        public string InternshipEmail { get; set; }
        public System.DateTime InternshipExpDate { get; set; }
		public string InternshipIndustry { get; internal set; }
		public string InternshipLocation { get; internal set; }
	}
}
