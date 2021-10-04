using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class InstituteAdmin
    {
        public int InstituteAdminID { get; set; }
        public Institute Institute { get; set; }
        public User User { get; set; }
    }
}
