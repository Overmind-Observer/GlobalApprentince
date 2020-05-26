namespace Global_Intern.Models
{
    public class InternStudent
    {
        public int InternStudentId { get; set; }
        public User User { get; set; } // selected user the intership
        public Internship Internship { get; set; }
    }
}
