using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string ProfileCV { get; set; }
        public string ProfileCoverLetter { get; set; }
        public User User { get; set; }
    }
}
