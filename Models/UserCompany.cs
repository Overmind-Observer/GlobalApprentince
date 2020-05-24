using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class UserCompany
    {
        public int UserCompanyId { get; set; }
        public string UserCompanyName { get; set; }
        public string UserCompanyType { get; set; }
        public string UserCompanyInfo { get; set; }
        public User User { get; set; }
    }
}
