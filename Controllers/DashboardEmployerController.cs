using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
using Global_Intern.Util;
using Global_Intern.Util.pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        public DashboardEmployerController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;
            

            // sets User _user using session
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            setUser(token);

        }

        [Authorize(Roles = "employer")]
        public IActionResult Index()
        {
            // Display User name on the right-top corner - shows user is logedIN
            ViewData["LoggeduserName"] = _user.UserFirstName + ' ' + _user.UserLastName;

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
            ViewData["LoggeduserName"] = _user.UserFirstName + ' ' + _user.UserLastName;

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
            string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
            ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);

            using (GlobalDBContext _context = new GlobalDBContext())
            {
                if (generalProfile.UserImage != null && generalProfile.UserImage.Length > 0)
                {
                    string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";

                    // File of code need to be Tested
                    //string file_Path = HelpersFunctions.StoreFile(uploadFolder, generalProfile.UserImage);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + generalProfile.UserImage.FileName;
                    string filePath = uploadFolder + uniqueFileName;
                    generalProfile.UserImage.CopyTo(new FileStream(filePath, FileMode.Create));

                    // Delete previous uploaded Image
                    if (!String.IsNullOrEmpty(_user.UserImage))
                    {
                        string imagePath = uploadFolder + _user.UserImage;
                        Directory.Delete(imagePath);
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

        //public async Task<IActionResult> InternshipsAsync([FromQuery]string search, int pageNumber = 0, int pageSize = 0)
        //{
        //    IEnumerable<Internship> model;
        //    HttpResponseMessage resp;
        //    string InternshipUrl = host + Internship_url;
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            InternshipUrl = InternshipUrl + "?search=" + search;
        //            if (pageNumber != 0 && pageSize != 0)
        //            {
        //                InternshipUrl += "&pageNumber=" + pageNumber.ToString() + "&pageSize=" + pageSize.ToString();
        //            }
        //        }
        //        else
        //        {
        //            if (pageNumber != 0 && pageSize != 0)
        //            {
        //                InternshipUrl += "?pageNumber=" + pageNumber.ToString() + "&pageSize=" + pageSize.ToString();
        //            }
        //        }
        //        resp = await _client.GetAsync(InternshipUrl);
        //        resp.EnsureSuccessStatusCode();
        //        string responseBody = await resp.Content.ReadAsStringAsync();
        //        var data = JsonConvert.DeserializeObject<dynamic>("[" + responseBody + "]");
        //        model = data[0]["data"].ToObject<IEnumerable<Internship>>();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return View(model);
        //}

        public void setUser(string token)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
                _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
            }
        }
        
        
        
        //Create CreateInternController  

        public IActionResult CreateInternController()
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
