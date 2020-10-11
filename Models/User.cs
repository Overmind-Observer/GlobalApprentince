using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
using Global_Intern.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Controllers
{
    [Authorize(Roles = "student")]
    public class DashboardStudentController : Controller
    {
        ////https://localhost:44307/api/internships/employer/3

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
        public List<UserDocument> UserDocuments { get; set; } // should get one row
        public List<UserCompany> UserCompanies { get; set; } // should get one row
        public List<Profile> Profiles { get; set; } // should get one row
        public List<InternStudent> InternStudents { get; set; } // list of students who are working in some internships
        public List<AppliedInternship> appliedInternships { get; set; } // list of user applyed for intership
        // add on 6th 10 2020
        public List<Course> Course { get; set; }


        public void AddFromAccountRegsiter(AccountRegister newUser, Role role, string salt)
        {
            this.UserFirstName = newUser.FirstName;
            this.UserLastName = newUser.LastName;
            this.UserGender = newUser.Gender;
            this.UserEmail = newUser.Email;
            this.UserEmailVerified = false;
            this.UserPassword = newUser.Password;
            this.Salt = salt;
            this.UserPhone = newUser.Phone.ToString();
            this.Role = role;
            this.CreatedAt = DateTime.UtcNow;
            this.SoftDelete = false;
        }
        public IActionResult Index()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

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
                // Geting internshps student applied for using his/her userID
                var appliedInterns = _context.AppliedInternships.Include(i => i.Internship).Where(e => e.User == _user).ToList();
                List<Internship> interns = new List<Internship>();
                foreach (var appliedIntern in appliedInterns)
                {
                    Internship theIntern = appliedIntern.Internship;
                    interns.Add(theIntern);
                }
            }
            return View();
        }


        //[Authorize]
        [HttpGet]
        public IActionResult GeneralProfile()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                ProfileViewStudent gen = new ProfileViewStudent(_user);

                return View(gen);
            }

        }

        [HttpPost]
        public IActionResult GeneralProfile(ProfileViewStudent fromData)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            // When Save button is clicked
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                if (fromData.UserImage != null && fromData.UserImage.Length > 0)
                {
                    string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fromData.UserImage.FileName;
                    string filePath = uploadFolder + uniqueFileName;
                    fromData.UserImage.CopyTo(new FileStream(filePath, FileMode.Create));

                    // Delete previous uploaded Image
                    if (!String.IsNullOrEmpty(_user.UserImage))
                    {
                        string imagePath = uploadFolder + _user.UserImage;
                        System.IO.File.Delete(imagePath);
                    }

                    // if new image is uploaded with other user info
                    _user.AddFromStudentProfileView(fromData, uniqueFileName);
                }
                else
                {
                    // Adding generalProfile attr to user without image
                    _user.AddFromStudentProfileView(fromData);
                }
                _context.Users.Update(_user);
                _context.SaveChanges();
                ProfileViewStudent gen = new ProfileViewStudent(_user);
                return View(gen);

            }
        }
        public IActionResult Qualifications()
        {
            // TODO
            return View();
        }
        [HttpPost]
        public IActionResult Qualifications(Qualification qualification)
        {
            // TODO
            return View();
        }

        public void setUser()
        {
            ///  Access "UserToken" Session. 
            /// NOTE:  Session get created when user login with unique id. This id is also used to identify the user from number of Auth Tokens
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            if (token == null) // if null user has not loggedIn
            {
                return;
            }
            using (GlobalDBContext _context = new GlobalDBContext())
            {

                if (_customAuthManager.Tokens.Count > 0)
                {
                    // check weather the unique id is in AuthManager
                    int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
                    // User is found in the AuthManager
                    _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
                }

            }
        }
    }
}
