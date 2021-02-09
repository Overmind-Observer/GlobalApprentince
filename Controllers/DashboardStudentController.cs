using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
using Global_Intern.Models.StudentModels;
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
        StudentInternProfile studentIntern = null;

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

        public void DashboardOptions()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);
        }
        public IActionResult Index()
        {
            DashboardOptions();

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
                ViewBag.IntershipsByLoginedInUser = _context.Internships.Where(e => e.User == _user).ToList();
            }
            return View();
        }


        //[Authorize]
        [HttpGet]
        public IActionResult GeneralProfile()
        {
            DashboardOptions();

            using GlobalDBContext context = new GlobalDBContext();
            
            var temp = context.StudentInternProfiles.Where(u=>u.User==_user).ToList();

            ConsoleLogs logs = new ConsoleLogs(_env);

            logs.WriteDebugLog(temp.Count().ToString());



            if (temp.Count() == 0)
            {
                ProfileViewStudent userViewModel = new ProfileViewStudent(_user);

                return View(userViewModel);
            }
            else
            {
                studentIntern = temp[0];

                ProfileViewStudent userViewModel = new ProfileViewStudent(_user, studentIntern);

                return View(userViewModel);
            }



            


         }

        [HttpPost]
        public IActionResult GeneralProfile(ProfileViewStudent updatedUser)
        {
            DashboardOptions();
            bool imageUploaded= true; ;
            StudentInternProfile internProfile = new StudentInternProfile();
            try
            {
                //if (updatedUser.UserImage!=null)
                //{
                //    if (_user.UserImage.Length > 0)
                //    {
                //        updatedUser.UserImageName = _user.UserImage;
                //    }
                //}
            }catch(Exception e)
            {
                //throw;
                imageUploaded = false;
                ConsoleLogs consoleLogs = new ConsoleLogs(_env);

                consoleLogs.WriteDebugLog(e.Message.ToString());
                consoleLogs.WriteDebugLog(e.StackTrace);
                consoleLogs.WriteDebugLog(e.Source);
                consoleLogs.WriteDebugLog(e.TargetSite.ToString());
                consoleLogs.WriteDebugLog(e.InnerException.ToString());

            }
            //-------------------- END
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                if (updatedUser.UserImage != null && updatedUser.UserImage.Length > 0)
                {
                    string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";

                    // File of code need to be Tested
                    //string file_Path = HelpersFunctions.StoreFile(uploadFolder, generalProfile.UserImage);





                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + updatedUser.UserImage.FileName;
                    // Delete previous uploaded Image
                    if (!String.IsNullOrEmpty(updatedUser.UserImage.ToString()))
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
                    updatedUser.UserImage.CopyTo(stream);
                    stream.Dispose();
                    //try
                    //{
                    //    if(updatedUser.UserImageName)
                    //}
                    updatedUser.UserImageName = uniqueFileName;

                    // if new image is uploaded with other user info
                    

                    
                }

                _user = _user.UpdateUserStudent(_user, updatedUser);

                //DateTime date = Convert.ToDateTime(Request.Form["UserDob"]);

                //DateTime date1 = Convert.ToDateTime(Request.Form["userVisaExpiryId"]);

                ProfileViewStudent profileView = new ProfileViewStudent();

                GlobalDBContext context = new GlobalDBContext();

                var temp = context.StudentInternProfiles.Where(u => u.User == _user).ToList();


                if (temp.Count==0)
                {
                    internProfile = profileView.updateOrCreateStudentInternProfile(internProfile, updatedUser, _user);
                    _context.Users.Update(_user);
                    _context.StudentInternProfiles.Add(internProfile);
                    _context.SaveChanges();
                }
                else
                {
                    internProfile = temp[0];
                    internProfile = profileView.updateOrCreateStudentInternProfile(internProfile, updatedUser, _user);
                    _context.Users.Update(_user);
                    _context.StudentInternProfiles.Update(internProfile);
                    _context.SaveChanges();
                }
                

                




                ViewBag.Message = updatedUser.UserFirstName + " " + updatedUser.UserLastName + " has been updated successfully. Check the Users table to see if it has been updated.";

                var temp1 = _context.Users.Find(_user.UserId);

                var temp2 = context.StudentInternProfiles.Where(u => u.User == _user).ToList();

                var temp3 = temp2[0];

                ProfileViewStudent userViewModel = new ProfileViewStudent(temp1,temp3);

                    return View(userViewModel);
            }

        }


        public IActionResult Settings()
        {
            DashboardOptions();

            ViewBag.Message = "Are you sure you want to delete user " + _user.UserFirstName + " " + _user.UserLastName;

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

        public void setUser()
        {
            ///  Access "UserToken" Session. 
            /// NOTE:  Session get created when user login with unique id. This id is also used to identify the user from number of Auth Tokens
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            if (token == null) // if null user has not loggedIn
            {
                return;
            }
            using (GlobalDBContext tooMuchConfusion = new GlobalDBContext())
            {

                if (_customAuthManager.Tokens.Count > 0)
                {
                    // check weather the unique id is in AuthManager
                    int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
                    // User is found in the AuthManager
                    _user = tooMuchConfusion.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
                }

            }
        }


        
        
        
        // Qualifications page works on 17 Oct 2020.
        public IActionResult Qualifications()
        {
            DashboardOptions();


            return View();

            

        }
        [HttpPost]
        public IActionResult Qualifications(Qualification qualification)
        {
            DashboardOptions();

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                // additional codes?
            }

            return View();
        }


        // Documents page works on 17 Oct 2020.
        public IActionResult Documents()
        {
            DashboardOptions();

            return View();
        }




        // Experience page works on 17 Oct 2020.
        public IActionResult Experience()
        {
            DashboardOptions();

            return View();
        }



        // MyApplications page works on 17 Oct 2020.
        public IActionResult MyApplications()
        {

            DashboardOptions();

            return View();
        }
        
        
        
    }
}
