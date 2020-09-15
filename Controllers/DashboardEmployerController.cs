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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Global_Intern.Controllers
{

    public class DashboardEmployerController : Controller
    {

        ////https://localhost:44307/api/internships/employer/3
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        private readonly IWebHostEnvironment _env;


        private User _user;
        private UserCompany _company;

        public DashboardEmployerController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;


            // sets User _user using session
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            setUser(token);
            setUserCompany();
        }

        [Authorize(Roles = "employer")]
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

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                ProfileViewEmployer userViewModel = new ProfileViewEmployer(_user);
                return View(userViewModel);
            }
        }

        [HttpPost]
        public IActionResult GeneralProfile(ProfileViewEmployer fromData)
        {

            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };
            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);
            //-------------------- END

            using (GlobalDBContext _context = new GlobalDBContext())
            {
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
                return View(fromData);
            }

        }


        public IActionResult CompanyDetails()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
            {
               
                UserCompany companyInfo = _context.UserCompany.Include(u => u.User).FirstOrDefault(e => e.User.UserId == _user.UserId);
                Global_Intern.Models.EmployerModels.EmployerCompanyModel model = new Models.EmployerModels.EmployerCompanyModel();
                if (companyInfo != null)
                {
                   model = new Models.EmployerModels.EmployerCompanyModel(companyInfo);
                }
                return View(model);
            }
            
        }
        [HttpPost]
        public IActionResult CompanyDetails(EmployerCompanyModel FromData)
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

            // Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                
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

        }


        public void setUser(string token)
        {
            
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                /// TESTING
                _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == 3);
                
                //int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
                //_user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
            }
        }

        // User should be initialized before using this method
        public void setUserCompany()
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                _company = _context.UserCompany.Include(r => r.User).FirstOrDefault(u => u.User.UserId == _user.UserId);
                if(_company == null)
                {
                    // inserting compnay 
                    UserCompany userCompany = new UserCompany();
                    User user = _context.Users.Find(_user.UserId);
                    //user.Role = null;
                    //userCompany.UserCompanyId = null;
                    //userCompany.User = user;
                    userCompany.User = user;
                    _context.UserCompany.Add(userCompany);
                    _context.SaveChanges();
                    _company = userCompany;
                }
            }
        }


        //Create CreateInternController  

        public IActionResult CreateInternship()
        {
            return View();
        }

        /*
                public IActionResult CreateInternController(AccountRegister new_user)
                {
                    using (GlobalDBContext _context = new GlobalDBContext())
                    {

                        public int InternshipId;
                        public string InternshipTitle;
                        public string InternshipType;
                        public string InternshipDuration;
                        public string InternshipBody;
                        public bool InternshipVirtual;
                        public string InternshipPaid;
                        public string InternshipEmail;
                        public System.DateTime InternshipExpDate;
                        public System.DateTime InternshipCreatedAt;
                        public System.DateTime InternshipUpdatedAt;


                        int InternshipID = new id;


                        Console.Write("Internship Title: ");
                        InternshipTitle = Console.ReadLine();

                        Console.Write("Internship Type: ");
                        InternshipType = Console.ReadLine();

                        Console.Write("Internship Duration: ");
                        InternshipDuration = Console.ReadLine();

                        Console.Write("Internship Body: ");
                        InternshipBody = Console.ReadLine();
                    }
                    return View();
                }
                */


    }
}
