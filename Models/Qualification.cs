using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models
{
	public class Qualification
	{
		public int QualificationId { get; set; }
		public string QualificationSchool { get; set; } // bachelor's,  Masters Phd -> Via a static DropDownList
		public string QualificationTitle { get; set; }
		public string QualificationType { get; set; }
		public string FieldofStudy { get; set; }
		public string Grade { get; set; }
		public System.DateTime StartDate { get; set; }
		public System.DateTime EndDate { get; set; }
		public bool QualificationStillStudying { get; set; }
		[Required]
		public virtual User User { get; set; }

		public string QualificationLevel { get; set; }

		public Qualification()
		{
			//QualificationSchool = "";
			//QualificationTitle = "";
			//QualificationType = "";
			//FieldofStudy = "";
			//Grade = "";
			//StartDate = str("");
			//EndDate = "";
			//QualificationStillStudying = "";
			//User = "";
		}

		public Qualification(string qualificationSchool, string qualificationTitle, string qualificationType, string fieldOfStudy, string grade, DateTime startDate, DateTime endDate, bool qualificationStillStudying, User user)
		{
			QualificationSchool = qualificationSchool;
			QualificationTitle = qualificationTitle;
			QualificationType = qualificationType;
			FieldofStudy = fieldOfStudy;
			Grade = grade;
			StartDate = startDate;
			EndDate = endDate;
			QualificationStillStudying = qualificationStillStudying;
			User = user;
		}

		public void AddNewQualification(Qualification NewQualification, User user)
		{						
			this.QualificationSchool = NewQualification.QualificationSchool;
			this.QualificationTitle = NewQualification.QualificationTitle;
			this.QualificationType = NewQualification.QualificationType;
			this.FieldofStudy = NewQualification.FieldofStudy;
			this.Grade = NewQualification.Grade;
			this.StartDate = NewQualification.StartDate;
			this.EndDate = NewQualification.EndDate;
			this.QualificationStillStudying = NewQualification.QualificationStillStudying;
			this.User = user;

		}

		public Qualification UpdateQualification(Qualification qualification, Qualification UpdatedQualification)
		{
			qualification.QualificationSchool = UpdatedQualification.QualificationSchool;
			qualification.QualificationTitle = UpdatedQualification.QualificationTitle;
			qualification.QualificationType = UpdatedQualification.QualificationType;
			qualification.FieldofStudy = UpdatedQualification.FieldofStudy;
			qualification.Grade = UpdatedQualification.Grade;
			qualification.StartDate = UpdatedQualification.StartDate;
			qualification.EndDate = UpdatedQualification.EndDate;
			qualification.QualificationStillStudying = UpdatedQualification.QualificationStillStudying;
			return qualification;
		}

	}
}
