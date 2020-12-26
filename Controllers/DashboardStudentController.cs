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
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Global_Intern.Controllers
{
    [Authorize(Roles = "student")]
    public class DashboardStudentController : Controller
    {
        ////https://localhost:44307/api/internships/employer/3

        private readonly IHttpContextAccessor _httpContextAccessor; // Accessor allows access to session and cookies
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;

        private readonly HttpClient _client = new HttpClient(); // Used to access API -> Internship.
        //private readonly string Internship_url = "/api/Internships"; - does not used!?
        IWebHostEnvironment _env; // to access the Content PATH aka wwwroot
        /// <summary>
        /// User object is quite important here. without accessing database again and again on every action. User is set on constructor level.
        /// </summary>
        private User _user;
        public DashboardStudentController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;

            // gets Session or host name 
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;

            // To Access runtime tokens
            _customAuthManager = auth;

            setUser();
        }
        public IActionResult Index()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
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


        public IActionResult Settings()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            ViewBag.Message = "Are you sure you want to delete user " + _user.UserFirstName + " " + _user.UserLastName;

            return View();
        }

        public IActionResult DeleteUser()
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {

                var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

                User user = _context.Users.Find(User_id);

                _context.Users.Remove(user);

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

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
        
        
        
        // Qualifications page works on 17 Oct 2020.
        public IActionResult Qualifications()
        {
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();

        }
        [HttpPost]
        public IActionResult Qualifications(Qualification qualification)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                // additional codes?
            }

            return View();
        }


        // Documents page works on 17 Oct 2020.
        public IActionResult Documents()
        {
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();
        }




        // Experience page works on 17 Oct 2020.
        public IActionResult Experience()
        {
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();
        }



        // MyApplications page works on 17 Oct 2020.
        public IActionResult MyApplications()
        {
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();
        }
    }
}
