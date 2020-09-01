using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.GeneralProfile
{
    public class GeneralProfile
    {
        public GeneralProfile()
        {

        }
        public GeneralProfile(User user)
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
          //Add on 1st September 2020
            UserStudentType = user.UserStudentType;
            UserWorkingRight = user.UserWorkingRight;
            UserVisaType = user.UserVisaType;
            UserVisaExpire = user.UserVisaExpire;
            UserIndustryCertificates = user.UserIndustryCertificates;
            UserDriverType = user.UserDriverType;
            UserDob = user.UserDob;
            UserEthnic = user.UserEthnic;
            
            
    }
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
        public string UserDob { get; set; }
        public string UserEthnic { get; set; }
        
        
    }
}
