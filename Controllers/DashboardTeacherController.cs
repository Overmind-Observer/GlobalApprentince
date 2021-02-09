using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
using Global_Intern.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public void DashboardOptions()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);
        }

        // This is for Dashboard Teacher Page 
        [Authorize(Roles = "teacher")]
        public IActionResult Index()
        {
            DashboardOptions();

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                // Gets all internship created by the user
                var course = _context.Course.ToList();
                return View(course);
            }
        }

        // Dashboard Teacher General Profile Page.
        public IActionResult GeneralProfile()
        {
            DashboardOptions();

            using (GlobalDBContext context = new GlobalDBContext())
            {

                ProfileViewTeacher teacher = new ProfileViewTeacher(_user);
                

                return View(teacher);

            }


        }

        //public async Task<IActionResult> Kopl()
        [HttpPost]
        public IActionResult GeneralProfile(ProfileViewTeacher UpdatedUser)
        {
            DashboardOptions();
            //-------------------- END
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                if (UpdatedUser.UserImage != null && UpdatedUser.UserImage.Length > 0)
                {
                    string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";

                    // File of code need to be Tested
                    //string file_Path = HelpersFunctions.StoreFile(uploadFolder, generalProfile.UserImage);





                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + UpdatedUser.UserImage.FileName;
                    // Delete previous uploaded Image
                    if (!String.IsNullOrEmpty(UpdatedUser.UserImage.ToString()))
                    {
                        string imagePath = uploadFolder + _user.UserImage;
                        if (System.IO.File.Exists(imagePath))
                        {
                            // If file found, delete it    
                            System.IO.File.Delete(imagePath);
                            Console.WriteLine("File deleted.");
                        }
                    }
                    string filePath = uploadFolder + uniqueFileName;
                    FileStream stream = new FileStream(filePath, FileMode.Create);
                    UpdatedUser.UserImage.CopyTo(stream);
                    stream.Dispose();
                    UpdatedUser.UserImageName = uniqueFileName;

                    // if new image is uploaded with other user info
                    _user = _user.UpdateUserTeacher(_user, UpdatedUser);
                }

                ViewBag.Message = UpdatedUser.UserFirstName + " " + UpdatedUser.UserLastName + " has been updated successfully. Check the Users table to see if it has been updated.";

                _context.Users.Update(_user);
                _context.SaveChanges();

                ProfileViewTeacher userViewModel = new ProfileViewTeacher(_user);
                return View(userViewModel);
            }

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
            DashboardOptions();

            return View();
        }

        [HttpPost]
        public IActionResult CreateCourses(Course NewCourse)
        {
            DashboardOptions();

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

        

        
        public async Task<IActionResult> Course(int id)
        {
            DashboardOptions();
            
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

        public IActionResult UpdateCourse(int id)
        {
            DashboardOptions();

            _httpContextAccessor.HttpContext.Session.SetString("CourseId", Convert.ToString(id));

            using (GlobalDBContext context = new GlobalDBContext())
            {
                Course course = context.Course.Find(id);

                return View(course);
            }

            
        }

        [HttpPost]
        public IActionResult UpdateCourse(Course UpdatedCourse)
        {
            DashboardOptions();

            using (GlobalDBContext context = new GlobalDBContext())
            {

                CourseId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("CourseId"));

                Course _course = context.Course.FirstOrDefault(k => k.CourseId == CourseId);

                Course course = new Course();

                course = course.UpdateCourse(_course, UpdatedCourse);

                context.Course.Update(course);

                context.SaveChanges();

                ViewBag.Message = "The course " + course.CourseTitle + " has been updated successfully";

                return View(course);
            }
        }

        [Route("{controller}/{Action}")]
        public IActionResult Settings()
        {
            DashboardOptions();

            ViewBag.Message = "Are you sure you want to delete user "+_user.UserFirstName + " " + _user.UserLastName;

            ViewBag.DeleteItem = "DeleteUser";

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

        [Route("{controller}/{Action}/{id}")]
        public IActionResult Settings(int id)
        {
            DashboardOptions();

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                Course course = _context.Course.Find(id);

                ViewBag.Message = "Are you sure you want to delete this Course " + course.CourseTitle+"?";
            }

            ViewBag.DeleteItem = "DeleteCourse";

            return View();
        }

        public IActionResult DeleteCourse()
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {

                var id = _httpContextAccessor.HttpContext.Session.GetString("DeleteCourseId");

                Course course = _context.Course.Find(Convert.ToInt32(id));

                _context.Course.Remove(course);

                _context.SaveChanges();

                return RedirectToAction("Index", "DashboardTeacher");
            }

        }




        public IActionResult Subscribers()
        {
            DashboardOptions();

            return View();
        }
    }
}
