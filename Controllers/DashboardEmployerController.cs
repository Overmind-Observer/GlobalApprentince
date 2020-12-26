using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.EmployerModels;
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
    [Authorize(Roles = "employer")]
    public class DashboardEmployerController : Controller
    {

        ////https://localhost:44307/api/Internships/employer/3
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        //private readonly string Internship_url = "/api/Internships"; - does not used!?
        private readonly IWebHostEnvironment _env;
        private readonly string Internship_url = "/api/Internships";
        string InternshipId;


        private User _user;
        private UserCompany _company;


        public DashboardEmployerController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;


            // sets User _user using session
            
            SetUser();
            SetUserCompany();
        }

        
        public IActionResult Index()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using GlobalDBContext _context = new GlobalDBContext();
            // Gets all Internship created by the user
            var Internship = _context.Internships.ToList();



            return View(Internship);
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
            //-------------------- END

            using GlobalDBContext _context = new GlobalDBContext();
            ProfileViewEmployer userViewModel = new ProfileViewEmployer(_user);
            return View(userViewModel);
        }

        //public IActionResult Internships()
        //{            // Display User name on the right-top corner - shows user is logedIN
        //    ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };
        //    // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
        //    string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
        //    ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);
        //    //-------------------- END

        //    using (GlobalDBContext _context = new GlobalDBContext())
        //    {
        //        Internship Internship = new Internship();
        //        return View(Internship);
        //    }
        //}

        [HttpPost]
        public IActionResult GeneralProfile(ProfileViewEmployer fromData)
        {
            using GlobalDBContext _context = new GlobalDBContext();
            if (fromData.UserImage != null && fromData.UserImage.Length > 0)
            {
                string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";

                // File of code need to be Tested
                //string file_Path = HelpersFunctions.StoreFile(uploadFolder, generalProfile.UserImage);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fromData.UserImage.FileName;
                string filePath = uploadFolder + uniqueFileName;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                fromData.UserImage.CopyTo(stream);
                stream.Dispose();

                // Delete previous uploaded Image
                if (!String.IsNullOrEmpty(_user.UserImage))
                {
                    string imagePath = uploadFolder + _user.UserImage;
                    if (System.IO.File.Exists(imagePath))
                    {
                        // If file found, delete it    
                        System.IO.File.Delete(imagePath);
                        Console.WriteLine("File deleted.");
                    }
                }
                // if new image is uploaded with other user info
                _user.AddFromEmployerProfileView(fromData, uniqueFileName);
            }
            else
            {
                // Adding generalProfile attr to user without image
                _user.AddFromEmployerProfileView(fromData);
            }
            _context.Users.Update(_user);
            _context.SaveChanges();
            fromData.UserImageName = _user.UserImage;

            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };
            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);
            //-------------------- END

            return View(fromData);

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
            using GlobalDBContext _context = new GlobalDBContext();
            var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

            User user = _context.Users.Find(User_id);

            _context.Users.Remove(user);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
        [Route("{controller}/{Action}/{id}")]
        public IActionResult Settings(int id)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            _httpContextAccessor.HttpContext.Session.SetString("DeleteInternshipId", Convert.ToString(id));

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                Internship Internship = _context.Internships.Find(id);

                ViewBag.Message = "Are you sure you want to delete this Internship " + Internship.InternshipTitle + "?";
            }

            ViewBag.DeleteItem = "DeleteInternship";

            return View();
        }

        public IActionResult DeleteInternship()
        {
            using GlobalDBContext _context = new GlobalDBContext();
            var id = _httpContextAccessor.HttpContext.Session.GetString("DeleteInternshipId");

            Internship Internship = _context.Internships.Find(Convert.ToInt32(id));

            _context.Internships.Remove(Internship);

            _context.SaveChanges();

            return RedirectToAction("Index", "DashboardTeacher");

        }

        public IActionResult CreateInternship()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();
        }

        [HttpPost]
        public IActionResult CreateInternship(Internship NewInternship)
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

                Internship nInternship = new Internship();

                User user = _context.Users.Find(User_id);

                //this creates new Internship
               nInternship= nInternship.CreateInternship(user, NewInternship);    // no definition!?

                _context.Internships.Add(nInternship);

                _context.SaveChanges();

                ViewBag.Message = NewInternship.InternshipTitle + " successfully created check the Internship table to see if it has been created" + WhatIsThis + "//" + WhatIsThis1;

            }
            return View();
        }

        public async Task<IActionResult> AllInternships([FromQuery] string searchTerm, int pageNumber = 0, int pageSize = 0)
        {
            /// What is happening How you get Internship -> Database to API. Api to you as a JSON response. when you can do something with that response
            /// In my custom response you get number of items, also you get what page you are on. And how many page to expect.
            /// Using QueryString, you can tell api what page you want, and how many Internship info you want.

            IEnumerable<Internship> model;
            HttpResponseMessage resp;
            string InternshipUrl = host + Internship_url;
            string tempInternshipUrl;

            ViewData["SearchTerm"] = searchTerm;
            try
            {

                if (!String.IsNullOrEmpty(searchTerm))
                {
                    InternshipUrl = InternshipUrl + "?search=" + searchTerm;
                    //InternshipUrl = InternshipUrl;
                    if (pageNumber != 0 && pageSize != 0)
                    {
                        InternshipUrl += "&pageNumber=" + pageNumber.ToString() + "&pageSize=" + pageSize.ToString();

                    }
                }
                else
                {
                    if (pageNumber != 0 && pageSize != 0)
                    {
                        InternshipUrl += "?pageNumber=" + pageNumber.ToString() + "&pageSize=" + pageSize.ToString();
                    }
                }

                resp = await _client.GetAsync(InternshipUrl);
                resp.EnsureSuccessStatusCode();
                string responseBody = await resp.Content.ReadAsStringAsync();
                if (responseBody == "400")
                {
                    ModelState.AddModelError("KeywordNotFound", "No Internships match the entered keyword.");
                    tempInternshipUrl = InternshipUrl.Replace("?search=" + searchTerm, null);
                    InternshipUrl = tempInternshipUrl;
                    resp = await _client.GetAsync(InternshipUrl);
                    resp.EnsureSuccessStatusCode();
                    responseBody = await resp.Content.ReadAsStringAsync();
                }
                var data = JsonConvert.DeserializeObject<dynamic>("[" + responseBody + "]");
                ViewBag.pageSize = data[0]["pageSize"];
                ViewBag.totalPages = data[0]["totalPages"];
                ViewBag.currentPage = data[0]["pageNumber"];
                model = data[0]["data"].ToObject<IEnumerable<Internship>>();
                var intern = data[0]["data"][0];
                return View(model);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Internship(int id)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            Internship model;
            HttpResponseMessage resp;
            string InternshipUrl = host + Internship_url;
            string temp = Convert.ToString(id);
            _httpContextAccessor.HttpContext.Session.SetString("InternshipId", temp);
            try
            {

                resp = await _client.GetAsync(InternshipUrl + "/" + id.ToString());
                resp.EnsureSuccessStatusCode();
                string responseBody = await resp.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>("[" + responseBody + "]");
                model = data[0].ToObject<Internship>();
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult UpdateInternship(int id)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            _httpContextAccessor.HttpContext.Session.SetString("InternshipId",Convert.ToString(id));

            using GlobalDBContext context = new GlobalDBContext();
            Internship Internship = context.Internships.Find(Convert.ToInt32(id));

            return View(Internship);


        }

        [HttpPost]
        public IActionResult UpdateInternship(Internship UpdatedInternship)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using GlobalDBContext context = new GlobalDBContext();
            InternshipId = _httpContextAccessor.HttpContext.Session.GetString("InternshipId");

            Internship _Internship = context.Internships.FirstOrDefault(k => k.InternshipId == Convert.ToInt32(InternshipId));

            Internship Internship = new Internship();

            Internship = Internship.UpdateInternship(_Internship, UpdatedInternship);

            context.Internships.Update(Internship);

            context.SaveChanges();

            ViewBag.Message = "The Internship " + Internship.InternshipTitle + " has been updated successfully";

            return View(Internship);
        }

        public IActionResult CompanyDetails()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using GlobalDBContext _context = new GlobalDBContext();
            UserCompany companyInfo = _context.UserCompany.Include(u => u.User).FirstOrDefault(e => e.User.UserId == _user.UserId);
            Global_Intern.Models.EmployerModels.EmployerCompanyModel model = new Models.EmployerModels.EmployerCompanyModel();
            if (companyInfo != null)
            {
                model = new Models.EmployerModels.EmployerCompanyModel(companyInfo);
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult CompanyDetails(EmployerCompanyModel FromData)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using GlobalDBContext _context = new GlobalDBContext();
            if (FromData.CompanyLogo != null && FromData.CompanyLogo.Length > 0)
            {
                string uploadFolder = _env.WebRootPath + @"\uploads\CompanyLogos\";

                // File of code need to be Tested
                //string file_Path = HelpersFunctions.StoreFile(uploadFolder, generalProfile.UserImage);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FromData.CompanyLogo.FileName;
                string filePath = uploadFolder + uniqueFileName;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                FromData.CompanyLogo.CopyTo(stream);
                stream.Dispose();
                // Delete previous uploaded Image
                if (!String.IsNullOrEmpty(_company.UserCompanyLogo))
                {
                    string imagePath = uploadFolder + _company.UserCompanyLogo;
                    if (System.IO.File.Exists(imagePath))
                    {

                        // If file found, delete it    
                        System.IO.File.Delete(imagePath);
                        Console.WriteLine("File deleted.");
                    }

                }
                // if new image is uploaded with other user info
                _company.AddFromEmployerCompanyModel(FromData, uniqueFileName);

            }
            else
            {
                // Adding generalProfile attr to user without image
                _company.AddFromEmployerCompanyModel(FromData);
            }
            _context.UserCompany.Update(_company);
            _context.SaveChanges();
            FromData.CompanyLogoName = _company.UserCompanyLogo;
            return View(FromData);

        }


        public void SetUser()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            if(token == null)
            {
                return;
            }
            using GlobalDBContext _context = new GlobalDBContext();
            if (_customAuthManager.Tokens.Count > 0)
            {
                int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
                _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
            }
        }

        // User should be initialized (setUser()) before using this method
        public void SetUserCompany()
        {
            if (_user == null) {
                return; 
            }
            using GlobalDBContext _context = new GlobalDBContext();
            _company = _context.UserCompany.Include(r => r.User).FirstOrDefault(u => u.User.UserId == _user.UserId);

            if (_company == null)  // If no record is found on user company
            {
                // inserting company with user ID 
                UserCompany userCompany = new UserCompany();
                User user = _context.Users.Find(_user.UserId);
                userCompany.User = user;
                _context.UserCompany.Add(userCompany);
                _context.SaveChanges();
                _company = userCompany;
            }
        }


        //Create CreateInternController  
        public IActionResult CreateInternships()
        {

            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            return View();
        }


        
        
        //From here is Intern Applications methods
        public IActionResult InternApplications()
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
