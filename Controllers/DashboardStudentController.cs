using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Global_Intern.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Global_Intern.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Global_Intern.Util;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Global_Intern.Models.GeneralProfile;
using System.IO;

namespace Global_Intern.Controllers
{
    [Authorize(Roles = "student")]
    public class DashboardStudentController : Controller
    {
        ////https://localhost:44307/api/internships/employer/3

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        IWebHostEnvironment _env;
        private User _user;
        public DashboardStudentController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;

            // gets Session or host name 
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            
            // To Access runtime tokens
            _customAuthManager = auth;

            // sets User _user using session
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            setUser(token);
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
                GeneralProfile gen = new GeneralProfile(_user);

                return View(gen);
            }

        }

        [HttpPost]
        public IActionResult GeneralProfile(GeneralProfile generalProfile)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            // When Save button is clicked
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                if (generalProfile.UserImage != null && generalProfile.UserImage.Length > 0)
                {
                    string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + generalProfile.UserImage.FileName;
                    string filePath = uploadFolder + uniqueFileName;
                    generalProfile.UserImage.CopyTo(new FileStream(filePath, FileMode.Create));

                    // Delete previous uploaded Image
                    if (!String.IsNullOrEmpty(_user.UserImage))
                    {
                        string imagePath = uploadFolder + _user.UserImage;
                        System.IO.File.Delete(imagePath);
                    }

                    // if new image is uploaded with other user info
                    _user.AddFromAccountGeneralProfile(generalProfile, uniqueFileName);
                }
                else
                {
                    // Adding generalProfile attr to user without image
                    _user.AddFromAccountGeneralProfile(generalProfile);
                }
                _context.Users.Update(_user);
                _context.SaveChanges();
                GeneralProfile gen = new GeneralProfile(_user);
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

        public void setUser(string token) {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
                _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
            }
        }
    }
}