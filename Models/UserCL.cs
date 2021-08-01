using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class UserCL
    {
        public int UserCLId { get; set; }
        public string UserCLFullPath { get; set; }
        public DateTime CLCreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
