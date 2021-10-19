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

		public void AddNewSkill(Skill NewSkill, StudentInternProfile intern)
		{
			this.SkillName = NewSkill.SkillName;
			this.SkillLevel = NewSkill.SkillLevel;
			this.Intern = intern;

		}
	}
}
