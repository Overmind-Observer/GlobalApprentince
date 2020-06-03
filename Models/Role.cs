using System;
using System.Collections.Generic;

namespace Global_Intern.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public String RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
