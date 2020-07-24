using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models
{
    public class UserCompany
    {
        public int UserCompanyId { get; set; }
        public string UserCompanyName { get; set; }
        public string UserCompanyLogo { get; set; }
        public string UserCompanyType { get; set; }
        public string UserCompanyInfo { get; set; }

        public string UserCompanyAddress { get; set; }
        public string UserCompanyState { get; set; }
        public string UserCompanyCountry { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
