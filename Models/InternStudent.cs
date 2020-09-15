namespace Global_Intern.Models
{
    public class InternStudent
    {
        // selected student for the intership
        public int InternStudentId { get; set; }
        public string IndemnityInsuranceDetails { get; set; }
        public virtual User User { get; set; }
        public virtual Internship Internship { get; set; }

    }
}
