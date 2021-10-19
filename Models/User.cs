using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Global_Intern.Models.GeneralProfile;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models
{
    public class User
    {
        public User()
        {
            this.CreatedAt = DateTime.UtcNow;
            UserCLs = new List<UserCL>();
        }
        // CP-> Complete Prfile - fields which will be filled after the email has been verfied
        // Auto -> fields which gets filled by the system

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public bool UserEmailVerified { get; set; } // Auto
        public string UserAddress { get; set; } //CP
        public string UserCity { get; set; } //CP
        public string UserState { get; set; } //CP
        public string UserCountry { get; set; } //CP
        public int UserZip { get; set; } //CP
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string Salt { get; set; } // Auto
        public string UniqueToken { get; set; } // Auto
        public string UserPhone { get; set; }
        public string UserImage { get; set; }
        public string UserGender { get; set; } // Could be use full for User with student role.



        [Required]
        public virtual Role Role { get; set; } //FK
        public DateTime CreatedAt { get; set; } // Auto
        public bool SoftDelete { get; set; } // Auto
        public List<Qualification> Qualifications { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<UserCompany> UserCompanies { get; set; } // should get one row
        public List<Profile> Profiles { get; set; } // should get one row
        public List<InternStudent> InternStudents { get; set; } // list of students who are working in some internships
        public List<AppliedInternship> appliedInternships { get; set; } // list of user applyed for intership
        public List<InstituteAdmin> InstituteAdmins { get; set; }
        public List<UserCL> UserCLs { get; set; }
        public List<UserCV> UserCVs { get; set; }

        // add on 6th 10 2020
        public List<Course> Course { get; set; }


        public void AddFromAccountRegister(User newUser, Role role, string salt)
        {
            this.UserFirstName = newUser.UserFirstName;
            this.UserLastName = newUser.UserLastName;
            this.UserGender = newUser.UserGender;
            this.UserEmail = newUser.UserEmail;
            this.UserEmailVerified = false;
            this.UserPassword = newUser.UserPassword;
            this.Salt = salt;
            this.UserPhone = newUser.UserPhone.ToString();
            this.Role = role;
            this.CreatedAt = DateTime.UtcNow;
            this.SoftDelete = false;
            //ProfileViewTeacher teacher = new 
        }

        public User UpdateUser(User _user,User user)
        {
            _user.UserFirstName = user.UserFirstName;
            _user.UserLastName = user.UserLastName;
            _user.UserAddress = user.UserAddress;
            _user.UserCity = user.UserCity;
            _user.CreatedAt = user.CreatedAt;
            _user.UserState = user.UserState;
            _user.UserCountry = user.UserCountry;
            _user.UserZip = user.UserZip;
            _user.UserImage = user.UserImage;
            _user.UserGender = user.UserGender;
            _user.UserPhone = user.UserPhone;
            if (user.UserImage != "")
            {
                _user.UserImage = user.UserImage;
            }

            return _user;
        }

        public User UpdateUserEmployer(User _user, GeneralProfile.ProfileViewEmployer user)
        {
            _user.UserFirstName = user.UserFirstName;
            _user.UserLastName = user.UserLastName;
            _user.UserAddress = user.UserAddress;
            _user.UserCity = user.UserCity;
            _user.UserState = user.UserState;
            _user.UserCountry = user.UserCountry;
            _user.UserZip = user.UserZip;
            _user.UserImage = user.UserImageName;
            _user.UserGender = user.UserGender;
            _user.UserPhone = user.UserPhone;
            if (_user.UserImage != "")
            {
                _user.UserImage = user.UserImageName;
            }

            return _user;
        }

        public User UpdateUserTeacher(User _user, GeneralProfile.ProfileViewTeacher user)
        {
            _user.UserFirstName = user.UserFirstName;
            _user.UserLastName = user.UserLastName;
            _user.UserAddress = user.UserAddress;
            _user.UserCity = user.UserCity;
            _user.UserState = user.UserState;
            _user.UserCountry = user.UserCountry;
            _user.UserZip = user.UserZip;
            _user.UserImage = user.UserImageName;
            _user.UserGender = user.UserGender;
            _user.UserPhone = user.UserPhone;
            if (_user.UserImage != "")
            {
                _user.UserImage = user.UserImageName;
            }

            return _user;
        }

        public User UpdateUserStudent(User _user, GeneralProfile.ProfileViewStudent user)
        {
            _user.UserFirstName = user.UserFirstName;
            _user.UserLastName = user.UserLastName;
            _user.UserAddress = user.UserAddress;
            _user.UserCity = user.UserCity;
            _user.UserState = user.UserState;
            _user.UserCountry = user.UserCountry;
            _user.UserZip = user.UserZip;
            _user.UserImage = user.UserImageName;
            _user.UserGender = user.UserGender;
            _user.UserPhone = user.UserPhone;
            if (_user.UserImage != "")
            {
                _user.UserImage = user.UserImageName;
            }

            return _user;
        }

        //public User UpdateUserWithImage(User _user, User user )
        //{
        //    _user.UserFirstName = user.UserFirstName;
        //    _user.UserLastName = user.UserLastName;
        //    _user.UserAddress = user.UserAddress;
        //    _user.UserCity = user.UserCity;
        //    _user.CreatedAt = user.CreatedAt;
        //    _user.UserState = user.UserState;
        //    _user.UserCountry = user.UserCountry;
        //    _user.UserZip = user.UserZip;
        //    _user.UserImage = user.UserImage;
        //    _user.UserGender = user.UserGender;
        //    _user.UserPhone = user.UserPhone;
        //    if (user.UserImage != "")
        //    {
        //        _user.UserImage = user.UserImage;
        //    }

        //    return _user;
        //}


        public void AddFromAccountGeneralProfile(Global_Intern.Models.GeneralProfile.ProfileViewDocuments updatedInfo, string UserImagePATH = "")
        {
            //this.UserFirstName = updatedInfo.UserFirstName;
            //this.UserLastName = updatedInfo.UserLastName;
            //this.UserGender = updatedInfo.UserGender;
            //this.UserPhone = updatedInfo.UserPhone.ToString();
            if (UserImagePATH != "")
            {
                this.UserImage = UserImagePATH;
            }
        }

        public void AddFromStudentProfileView(Global_Intern.Models.GeneralProfile.ProfileViewStudent updatedInfo, string UserImagePATH = "")
        {
            UserFirstName = updatedInfo.UserFirstName;
            UserLastName = updatedInfo.UserLastName;
            UserGender = updatedInfo.UserGender;
            UserPhone = updatedInfo.UserPhone.ToString();
            UserAddress = updatedInfo.UserAddress;
            UserCity = updatedInfo.UserCity;
            UserState = updatedInfo.UserState;
            UserCountry = updatedInfo.UserCountry;
            // if user upload new image
            if (UserImagePATH != "")
            {
                this.UserImage = UserImagePATH;
            }
        }


        public void AddFromEmployerProfileView(Global_Intern.Models.GeneralProfile.ProfileViewEmployer updatedInfo, string UserImagePATH = "")
        {
            UserFirstName = updatedInfo.UserFirstName;
            UserLastName = updatedInfo.UserLastName;
            UserGender = updatedInfo.UserGender;
            UserPhone = updatedInfo.UserPhone.ToString();
            UserAddress = updatedInfo.UserAddress;
            UserCity = updatedInfo.UserCity;
            UserState = updatedInfo.UserState;
            UserCountry = updatedInfo.UserCountry;
            if (UserImagePATH != "")
            {
                this.UserImage = UserImagePATH;
            }
        }


        // for Dashboard Teacher
        public void AddFromTeacherProfileView(User updatedInfo, string UserImagePATH = "")
        {
            this.UserFirstName = updatedInfo.UserFirstName;
            this.UserLastName = updatedInfo.UserLastName;
            this.UserGender = updatedInfo.UserGender;
            this.UserPhone = updatedInfo.UserPhone.ToString();
            this.UserAddress = updatedInfo.UserAddress;
            this.UserCity = updatedInfo.UserCity;
            this.UserState = updatedInfo.UserState;
            this.UserCountry = updatedInfo.UserCountry;
            //updatedInfo.Salt="jnjn";
            if (UserImagePATH != "sss")
            {
                this.UserImage = "iiiiiii";
            }
        }




    }
}
