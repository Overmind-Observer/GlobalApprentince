using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public String RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
