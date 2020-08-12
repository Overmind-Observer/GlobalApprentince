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

namespace Global_Intern.Controllers
{

    public class DashboardStudentController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        IWebHostEnvironment _env;
        User user;
        public DashboardStudentController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            
            _customAuthManager = auth;
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            var tok = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token);

            //https://localhost:44307/api/internships/employer/3
        }
        [Authorize(Roles = "student")]
        public IActionResult Index()
        {
            
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;
                User user = _context.Users.Find(User_id);

                // Display User name on the right-top corner - shows user is logedIN
                ViewData["LoggeduserName"] = user.UserFirstName + ' ' + user.UserLastName;

                // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
                string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
                ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(user.UserId, path);

                // Geting internshps student applied for using his/her userID
                var appliedInterns = _context.AppliedInternships.Include(i => i.Internship).Where(e => e.User == user).ToList();
                List<Internship> interns = new List<Internship>();
                foreach(var appliedIntern in appliedInterns)
                {
                    Internship theIntern = appliedIntern.Internship;
                    interns.Add(theIntern);
                }
            }
            return View();
        }

    }
}