using System.ComponentModel.DataAnnotations;
using Global_Intern.Models.StudentModels;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models.GeneralProfile
{
    public class ProfileViewStudent
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        //public string UserEmail { get; set; }
        public string UserAddress { get; set; } //CP
        public string UserCity { get; set; } //CP
        public string UserState { get; set; } //CP
        public string UserCountry { get; set; } //CP
        public int UserZip { get; set; }
        public string UserPhone { get; set; }
        public string UserImageName { get; set; }
        public IFormFile UserImage { get; set; }

        public string UserGender { get; set; }
        //Add on 1 Sep 2020
        public string UserStudentType { get; set; }
        public string UserWorkingRight { get; set; }
        public string UserVisaType { get; set; }
        public string UserVisaExpire { get; set; }
        public string UserIndustryCertificates { get; set; }
        public string UserDriverType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string UserDob { get; set; }
        public string UserEthnic { get; set; }

        public ProfileViewStudent()
        {

        }

        //public ProfileViewStudent(User user, ExtendedStudent info)

        public ProfileViewStudent(User user)
        {
            UserFirstName = user.UserFirstName;
            UserLastName = user.UserLastName;
            UserAddress = user.UserAddress;
            UserCity = user.UserCity;
            UserState = user.UserState;
            UserCountry = user.UserCountry;
            UserZip = user.UserZip;
            UserPhone = user.UserPhone;
            UserImageName = user.UserImage;
            UserGender = user.UserGender;
        }



        public ProfileViewStudent(User user, StudentInternProfile student)
        {
            UserFirstName = user.UserFirstName;
            UserLastName = user.UserLastName;
            UserAddress = user.UserAddress;
            UserCity = user.UserCity;
            UserState = user.UserState;
            UserCountry = user.UserCountry;
            UserZip = user.UserZip;
            UserPhone = user.UserPhone;
            UserImageName = user.UserImage;
            UserGender = user.UserGender;
            UserStudentType = student.StudentType;
            UserWorkingRight = student.StudentWorkingRight;
            UserVisaType = student.StudentVisaType;
            UserVisaExpire = student.StudentVisaExpire;
            UserIndustryCertificates = student.StudentIndustryCertificates;
            UserDriverType = student.StudentDriverType;
            UserDob = student.StudentDob;
            UserEthnic = student.StudentEthnic;
        }
        public StudentInternProfile updateOrCreateStudentInternProfile(StudentInternProfile newProfile, ProfileViewStudent createdProfile, User user)
        {
            newProfile.StudentType = createdProfile.UserStudentType;
            newProfile.StudentWorkingRight = createdProfile.UserWorkingRight;
            newProfile.StudentVisaType = createdProfile.UserVisaType;
            newProfile.StudentVisaExpire = createdProfile.UserVisaExpire;
            newProfile.StudentIndustryCertificates = createdProfile.UserIndustryCertificates;
            newProfile.StudentDriverType = createdProfile.UserDriverType;
            newProfile.StudentDob = createdProfile.UserDob;
            newProfile.StudentEthnic = createdProfile.UserEthnic;
            newProfile.User = user;
            return newProfile;
        }

        public void CreateOrUpdateDocuments()
        {

        }

        public StudentInternProfile updateStudentInternProfileOtherDetails(StudentInternProfile newProfile, ProfileViewStudent updatedProfile)
        {
            newProfile.StudentType = updatedProfile.UserStudentType;
            newProfile.StudentWorkingRight = updatedProfile.UserWorkingRight;
            newProfile.StudentVisaType = updatedProfile.UserVisaType;
            newProfile.StudentVisaExpire = updatedProfile.UserVisaExpire;
            newProfile.StudentIndustryCertificates = updatedProfile.UserIndustryCertificates;
            newProfile.StudentDriverType = updatedProfile.UserDriverType;
            newProfile.StudentDob = updatedProfile.UserDob;
            newProfile.StudentEthnic = updatedProfile.UserEthnic;
            
            return newProfile;
        }

    }
}
