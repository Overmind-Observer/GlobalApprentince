namespace Global_Intern.Models
{
    public class InternStudent
    {
        public int InternStudentId { get; set; }
        public virtual User User { get; set; } // selected user the intership
        public virtual Internship Internship { get; set; }
    }
}
