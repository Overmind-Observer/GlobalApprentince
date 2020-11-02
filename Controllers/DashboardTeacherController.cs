using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
using Global_Intern.Models.TeacherModels;
using Global_Intern.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Global_Intern.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize(Roles = "teacher")]
    public class DashboardTeacherController : Controller
    {


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        //private readonly GlobalDBContext _context;
        IWebHostEnvironment _env;
        private User _user;
        GlobalDBContext context = new GlobalDBContext();



        public DashboardTeacherController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env, GlobalDBContext context)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;
            


            // sets User _user using session
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            // To Access runtime tokens
            _customAuthManager = auth;

            setUser();
        }

        // setUser() method continue for above.
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

        // This is for Dashboard Teacher Page 
        [Authorize(Roles = "teacher")]
        public IActionResult Index()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                // Gets all internship created by the user
                ViewBag.IntershipsByLoginedInUser = _context.Internships.Where(e => e.User == _user).ToList();
                return View();
            }
        }

        // Dashboard Teacher General Profile Page.
        public IActionResult GeneralProfile()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext context = new GlobalDBContext())
            {

                User user = new User();

                return View(_user);

            }


        }

        //public async Task<IActionResult> Kopl()
        [HttpPost]
        public IActionResult GeneralProfile(User UpdateDetails)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

            GlobalDBContext _context = new GlobalDBContext();

                //assigns the new values to the updated ones
                _user.UserFirstName = UpdateDetails.UserFirstName;
                _user.UserLastName = UpdateDetails.UserLastName;
                _user.UserAddress = UpdateDetails.UserAddress;
                _user.UserCity = UpdateDetails.UserCity;
                _user.CreatedAt = UpdateDetails.CreatedAt;
                _user.UserState = UpdateDetails.UserState;
                _user.UserCountry = UpdateDetails.UserCountry;
                _user.UserZip = UpdateDetails.UserZip;
                _user.UserImage = UpdateDetails.UserImage;
                _user.UserGender = UpdateDetails.UserGender;               

                _context.Users.Update(_user);

                _context.SaveChanges();

                ViewBag.Message = _user.UserFirstName + " " + _user.UserLastName + " has been updated successfully. Check the Users table to see if it has been updated.";

                return View(_user);
            
        }



        public IActionResult Courses()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);


            return View();
        }

        public IActionResult CreateCourses()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();
        }

        [HttpPost]
        public IActionResult CreateCourses(Course NewCourse)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

            var WhatIsThis = _customAuthManager.Tokens.FirstOrDefault().Value.Item1;

            var WhatIsThis1 = _customAuthManager.Tokens.FirstOrDefault().Value.Item2;

            //var WhatIsThis2 = _customAuthManager.Tokens.FirstOrDefault().Value.GetType;

            using (GlobalDBContext _context = new GlobalDBContext())
            {

                Course nCourse = new Course();

                //User user = _context.Users.Find(User_id);

                User user = _context.Users.Find(User_id);

                //this creates new course
                nCourse.CreateNewCourse(NewCourse,user);

                _context.Course.Add(nCourse);

                _context.SaveChanges();

                ViewBag.Message = NewCourse.CourseTitle + " successfully created check the courses table to see if it has been created"+WhatIsThis+"//"+WhatIsThis1;

            }
            return View();
        }




        public IActionResult Subscribers()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);


            return View();
        }
    }
}
