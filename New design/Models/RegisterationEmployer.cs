using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Global_Intern.Models.GeneralProfile;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models
{
    public class RegisterationEmployer
    {

        public int UserId { get; set; }
        public virtual Role Role { get; set; }


        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Require alphabet only")]
        public string UserFirstName { get; set; }

        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Require alphabet only")]
        public string UserLastName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string UserEmail { get; set; }



        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(("^[0-9]+$"), ErrorMessage = "Number only.")]
        public int UserPhone { get; set; }



        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Organization Name is required")]
        public string UserOrganization { get; set; }


        [Display(Name = "NZBN Number")]
        [Required(ErrorMessage = "NZBN number is required ")]
        [RegularExpression(@"^[9]\d{12}", ErrorMessage = "Number only,13 digits and start with 9.")]
        public string UserNZBN { get; set; }


        [Display(Name = "Employees ")]
        [Required(ErrorMessage = "Please select no of Employees")]
        public string UserEmployees { get; set; }


        [Display(Name = "Industry ")]
        [Required(ErrorMessage = "Please select Industry")]
        public string UserIndustry { get; set; }



        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password must contain at least a number, one upper case and 8 characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string UserPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string UserCPassword { get; set; }








    }
}
