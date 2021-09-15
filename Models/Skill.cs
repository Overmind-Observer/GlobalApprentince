using Global_Intern.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
        public StudentInternProfile Intern { get; set; }
    }
}
