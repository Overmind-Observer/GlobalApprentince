using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Global_Intern.Models
{
    public class User
    {

        public User()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        public int UserId { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public bool UserEmailVerified { get; set; }
        public string UserHomeCountry { get; set; }
        public string UserCurrentCountry { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string Salt { get; set; }
        public string UserPhone { get; set; }
        public string UserLinks { get; set; }
        public string UserGender { get; set; }
        [Required]
        public virtual Role Role { get; set; } //FK
        public DateTime CreatedAt { get; set; }
        public bool SoftDelete { get; set; }
        public List<Qualification> qualifications { get; set; }
        public List<Experience> experiences { get; set; }
        public List<VisaStatus> visaStatuses { get; set; } // should get one row
        public List<UserCompany> userCompanies { get; set; } // should get one row
        public List<Profile> profiles { get; set; } // should get one row
        public List<InternStudent> internStudents { get; set; } // list of students who are working in some internships
        public List<AppliedInternship> appliedInternships { get; set; } // list of user applyed for intership


        public void AddFromAccountRegsiter(AccountRegister newUser, Role role, string salt)
        {
            this.UserFirstName  = newUser.FirstName;
            this.UserLastName   = newUser.LastName;
            this.UserGender     = newUser.Gender;
            this.UserEmail      = newUser.Email;
            this.UserEmailVerified = false;
            this.UserPassword   = newUser.Password;
            this.Salt           = salt;
            this.UserPhone      =  newUser.Phone.ToString();
            this.Role           = role;
            this.CreatedAt = DateTime.UtcNow;
            this.SoftDelete = false;
        }
    }
}
