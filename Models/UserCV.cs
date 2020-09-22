using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    // One User will have one CV entry           
    public class UserCV
    {
        public int UserCVId { get; set; }
        public string UserCVFullPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
