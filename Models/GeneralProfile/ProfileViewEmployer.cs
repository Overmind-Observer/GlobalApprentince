using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models.GeneralProfile
{
    public class ProfileViewEmployer
    {
        public ProfileViewEmployer()
        {

        }
        public ProfileViewEmployer(User user)
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
        public IFormFile UserImage { get; set; } // From POST
        public string UserGender { get; set; }

    }
}
