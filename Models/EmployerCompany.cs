namespace Global_Intern.Models
{
    public class EmployerCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
        public Employer Employer { get; set; }
    }
}
