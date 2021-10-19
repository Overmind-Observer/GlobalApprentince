using System;

namespace Global_Intern.Models
{
	public class Experience
	{
		public int ExperienceId { get; set; }
		public string ExperienceTitle { get; set; }
		public string ExperienceCompany { get; set; }
		public string ExperienceLocation { get; set; }
		public DateTime ExperienceStartDate { get; set; }
		public DateTime ExperienceEndDate { get; set; } // If Still working is true this field will go empty
		public bool ExperienceStillWorking { get; set; }
		public virtual User User { get; set; }


		public void AddNewExperience(Experience NewExperience, User user)
		{
			this.ExperienceTitle = NewExperience.ExperienceTitle;
			this.ExperienceCompany = NewExperience.ExperienceCompany;
			this.ExperienceLocation = NewExperience.ExperienceLocation;
			this.ExperienceStartDate = NewExperience.ExperienceStartDate;
			this.ExperienceEndDate = NewExperience.ExperienceEndDate;
			this.ExperienceStillWorking = NewExperience.ExperienceStillWorking;
			this.User = user;
		}

		public Experience UpdateExperience(Experience experience, Experience UpdatedExperience)
		{
			experience.ExperienceTitle = UpdatedExperience.ExperienceTitle;
			experience.ExperienceCompany = UpdatedExperience.ExperienceCompany;
			experience.ExperienceLocation = UpdatedExperience.ExperienceLocation;
			experience.ExperienceStartDate = UpdatedExperience.ExperienceStartDate;
			experience.ExperienceEndDate = UpdatedExperience.ExperienceEndDate;
			experience.ExperienceStillWorking = UpdatedExperience.ExperienceStillWorking;
			return experience;
		}
	}
}
