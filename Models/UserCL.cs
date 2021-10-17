using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public partial class UserCL
    {
        public int UserClid { get; set; }
        public string UserClfullPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
