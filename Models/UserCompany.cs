namespace Global_Intern.Models
{
    public class UserCompany
    {
        public int UserCompanyId { get; set; }
        public string UserCompanyName { get; set; }
        public string UserCompanyType { get; set; }
        public string UserCompanyInfo { get; set; }
        public virtual User User { get; set; }
    }
}
