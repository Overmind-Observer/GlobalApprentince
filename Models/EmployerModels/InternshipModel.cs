using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.EmployerModels
{
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
    }
}
