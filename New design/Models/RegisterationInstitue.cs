using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Global_Intern.Models.GeneralProfile;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models
{
    public class RegisterationInstitue
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

        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Require alphabet only")]
        public string Admin1Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Admin1Email { get; set; }

        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Require alphabet only")]
        public string Admin2Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Admin2Email { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(("^[0-9]+$"), ErrorMessage = "Number only.")]
        public int UserPhone { get; set; }


        [Display(Name = "University ")]
        [Required(ErrorMessage = "Please select University")]
        public string UserUniversity { get; set; }


        [Display(Name = "Based ")]
        [Required(ErrorMessage = "Please select based")]
        public string UserBased { get; set; }



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
