using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Institute
    {
        public int InstituteID { get; set; }
        public string InsituteName { get; set; }
        public string InstituteLocation { get; set; }
        public List<InstituteAdmin> InstituteAdmin { get; set; }
    }
}
