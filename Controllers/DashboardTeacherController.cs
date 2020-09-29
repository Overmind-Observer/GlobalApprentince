using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
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

namespace Global_Intern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardTeacherController : Controller
    {


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        IWebHostEnvironment _env;
        private User _user;



        public DashboardTeacherController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;


            // sets User _user using session
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            
            
        }

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








        // Dashboard Teacher controller start here.
        public IActionResult TeacherGeneralProfile()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);


            using (GlobalDBContext _context = new GlobalDBContext())
            {
                ProfileViewTeacher gen = new ProfileViewTeacher(_user);

                return View(gen);
            }

        }


        public IActionResult TeacherGeneralProfile(ProfileViewTeacher fromData)
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
                    _user.AddFromTeacherProfileView(fromData, uniqueFileName);
                }
                else
                {
                    // Adding generalProfile attr to user without image
                    _user.AddFromTeacherProfileView(fromData);
                }
                _context.Users.Update(_user);
                _context.SaveChanges();
                ProfileViewStudent gen = new ProfileViewStudent(_user);
                return View(gen);

            }
        }




    }
}
