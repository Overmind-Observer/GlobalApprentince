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
        public virtual User User { get; set; }
    }
}
