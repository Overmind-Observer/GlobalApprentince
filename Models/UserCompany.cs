using Global_Intern.Models.EmployerModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Global_Intern.Models
{
    public class UserCompany
    {
        public int? UserCompanyId { get; set; }
        public string UserCompanyName { get; set; }
        public string UserCompanyLogo { get; set; }
        public string UserCompanyType { get; set; }
        public string UserCompanyInfo { get; set; }

        public string UserCompanyAddress { get; set; }
        public string UserCompanyState { get; set; }
        public string UserCompanyCountry { get; set; }
        [Required]
        public virtual User User { get; set; }
        public UserCompany()
        {

        }
        public UserCompany(EmployerCompanyModel form, User _user, string fileName = null)
        {
            UserCompanyName = form.CompanyName;
            if (fileName != null)
            {
                UserCompanyLogo = fileName;
            }

            UserCompanyType = form.CompanyType;
            UserCompanyInfo = form.CompanyInfo;
            UserCompanyAddress = form.CompanyLocation;
            UserCompanyState = form.CompanyState;
            UserCompanyCountry = form.CompanyCountry;
            User = _user;
        }
        public void AddFromEmployerCompanyModel(EmployerCompanyModel form, string fileName = null)
        {
            UserCompanyName = form.CompanyName;
            if(fileName != null)
            {
                UserCompanyLogo = fileName;
            }

            UserCompanyType = form.CompanyType;
            UserCompanyInfo = form.CompanyInfo;
            UserCompanyAddress = form.CompanyLocation;
            UserCompanyState = form.CompanyState;
            UserCompanyCountry = form.CompanyCountry;
        }
    }
}
