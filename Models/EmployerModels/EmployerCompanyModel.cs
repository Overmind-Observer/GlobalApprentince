using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using System;
using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models.EmployerModels
{
    public class EmployerCompanyModel
    {
        public string CompanyName { get; set; }
        public IFormFile CompanyLogo { get; set; }

        public string CompanyLogoName { get; set; }
        public string CompanyType { get; set; }
        public string CompanyInfo { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyLocation { get; set; } // Address -> UserCompanyAddress
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }

        // No need for the fileds as it a viewModel for UserCompany Model

        //public string EmployerPostion { get; set; }
        //public string EmployerFristName { get; set; }
        //public string EmployerLastName { get; set; }
        //public string EmployerZip { get; set; }

        // This ctor used to set view model from UserCompany object

        public EmployerCompanyModel()
        {

        }
        public EmployerCompanyModel(UserCompany company)
        {
            CompanyName = company.UserCompanyName;
            CompanyLogoName = company.UserCompanyLogo;
            CompanyType = company.UserCompanyType;
            CompanyInfo = company.UserCompanyInfo;
            CompanyWebsite = "";
            CompanyLocation = company.UserCompanyAddress;
            CompanyState = company.UserCompanyState;
            CompanyCountry = company.UserCompanyCountry;
        }

        public void setLogoPath(string name)
        {
            CompanyLogoName = name;
        }
    }
}
