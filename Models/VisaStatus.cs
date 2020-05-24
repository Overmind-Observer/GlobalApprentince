using Publisher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class VisaStatus
    {
        public int VisaId { get; set; }
        public string VisaType { get; set; }
        public string VisaNumber { get; set; }
        public Student Student { get; set; }
    }
}
