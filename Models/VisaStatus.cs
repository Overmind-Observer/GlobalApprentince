namespace Global_Intern.Models
{
    public class VisaStatus
    {
        public int VisaStatusId { get; set; }
        public string VisaType { get; set; }
        public int VisaNumber { get; set; }
        public User User { get; set; }
    }
}
