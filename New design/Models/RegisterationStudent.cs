using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Global_Intern.Models.GeneralProfile;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models
{
    public class RegisterationStudent
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


        [Display(Name = "Gender ")]
        [Required(ErrorMessage = "Please select Gender")]
        public string UserGender { get; set; }


        [Display(Name = "Ethnicity ")]
        [Required(ErrorMessage = "Please select Ethnicity")]
        public string UserEthnicity { get; set; }


        [Display(Name = "University ")]
        [Required(ErrorMessage = "Please select University")]
        public string UserUniversity { get; set; } 

       
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
