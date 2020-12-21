using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        //private readonly string Internship_url = "/api/Internships"; - does not used!?
        //private readonly GlobalDBContext _context;
        IWebHostEnvironment _env;
        string Course_url = "/api/Course"; 
        private User _user;
        GlobalDBContext context = new GlobalDBContext();
        int CourseId;



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

            User user = new User();

            user = user.UpdateUser(_user, UpdateDetails);

                _context.Users.Update(user);

                _context.SaveChanges();

                ViewBag.Message = user.UserFirstName + " " + user.UserLastName + " has been updated successfully. Check the Users table to see if it has been updated.";

                return View(user);
            
        }



        //public IActionResult Courses()
        //{
        //    // Display User name on the right-top corner - shows user is logedIN
        //    ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

        //    // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
        //    string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
        //    ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

        //    Course course = new Course();

        //    ViewData["CoursesModel"] = course;

        //    GlobalDBContext context = new GlobalDBContext();

        //    ConsoleLogs consoleLogs = new ConsoleLogs(_env);

            

        //    for (var k=0;k<16;k++)
        //    {
        //        consoleLogs.WriteErrorLog(context.Users.ToList().ToString());
        //    }



        //    return View();
        //}

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

                User user = _context.Users.Find(User_id);

                //this creates new course
                nCourse.CreateNewCourse(NewCourse,user);    // no definition!?

                _context.Course.Add(nCourse);

                _context.SaveChanges();

                ViewBag.Message = NewCourse.CourseTitle + " successfully created check the courses table to see if it has been created"+WhatIsThis+"//"+WhatIsThis1;

            }
            return View();
        }

        public async Task<IActionResult> AllCourses([FromQuery] string searchTerm, int pageNumber = 0, int pageSize = 0)
        {
            //Course model;
            IEnumerable<Course> model;
            HttpResponseMessage resp;
            String CourseUrl = host + Course_url;
            string tempCourseUrl;
            ViewData["SearchTerm"] = searchTerm;
            try
            {
                if (!String.IsNullOrEmpty(searchTerm))
                {
                    CourseUrl = CourseUrl + "?search=" + searchTerm;
                    if (pageNumber != 0 && pageSize != 0)
                    {
                        CourseUrl += "&pageNumber" + pageNumber.ToString() + "&pageSize" + pageSize.ToString();
                    }
                }
                else
                {
                    if (pageNumber != 0 && pageSize != 0)
                    {
                        CourseUrl += "?pageNumber" + pageNumber.ToString() + "&pageSize" + pageSize.ToString();
                    }
                }



                resp = await _client.GetAsync(CourseUrl);
                resp.EnsureSuccessStatusCode();
                string responseBody = await resp.Content.ReadAsStringAsync();
                if (responseBody == "400")
                {
                    ModelState.AddModelError("KeywordNotFound", "No Internships match the entered keyword.");
                    tempCourseUrl = CourseUrl.Replace("?search=" + searchTerm, null);
                    CourseUrl = tempCourseUrl;
                    resp = await _client.GetAsync(CourseUrl);
                    resp.EnsureSuccessStatusCode();
                    responseBody = await resp.Content.ReadAsStringAsync();
                }
                var data = JsonConvert.DeserializeObject<dynamic>("[" + responseBody + "]");
                ViewBag.pageSize = data[0]["pageSize"];
                ViewBag.totalPages = data[0]["totalPages"];
                ViewBag.currentPage = data[0]["pageNumber"];
                model = data[0]["data"].ToObject<IEnumerable<Course>>();
                var course = data[0]["data"][0];
                return View(model);
            }
            catch (Exception) //removed unused variable ex
            {

                throw;
            }

        }

        
        public async Task<IActionResult> Courses(int id)
        {
            Course model;
            HttpResponseMessage resp;
            string CourseUrl = host + Course_url;
            string temp = Convert.ToString(id);
            _httpContextAccessor.HttpContext.Session.SetString("CourseId", temp);

            try
            {
                resp = await _client.GetAsync(CourseUrl + "/" + id.ToString());
                resp.EnsureSuccessStatusCode();
                string responseBody = await resp.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>("[" + responseBody + "]");
                model = data[0].ToObject<Course>();
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IActionResult UpdateCourse()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            CourseId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("CourseId"));

            using (GlobalDBContext context = new GlobalDBContext())
            {
                Course course = context.Course.Find(CourseId);

                return View(course);
            }

            
        }

        [HttpPost]
        public IActionResult UpdateCourse(Course UpdatedCourse)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using(GlobalDBContext context = new GlobalDBContext())
            {

                CourseId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("CourseId"));

                Course _course = context.Course.FirstOrDefault(k => k.CourseId == CourseId);

                Course course = new Course();

                course = course.UpdateCourse(_course, UpdatedCourse);

                context.Course.Update(course);

                context.SaveChanges();

                return View(course);
            }
        }


        public IActionResult Settings()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            ViewBag.Message = "Are you sure you want to delete user "+_user.UserFirstName + " " + _user.UserLastName;

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
