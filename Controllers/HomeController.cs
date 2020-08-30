﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Global_Intern.Models;
using Global_Intern.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http; // for -> IHttpContextAccessor
using System.Net.Http; // for -> HttpClient to make request to API
using Global_Intern.Util;
using Global_Intern.Models.StudentModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Global_Intern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        private User _user = null;
        IWebHostEnvironment _env;
        public HomeController(ILogger<HomeController> logger,
            IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth,
            IWebHostEnvironment env)
        {
            _customAuthManager = auth;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {

            CookieLogin();

            // For NavBar to display user is LoggedIn
            if (_user != null)
            {
                ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };
            }


            return View();
        }

        public async Task<IActionResult> AllInternships([FromQuery]string search, int pageNumber = 0, int pageSize = 0)
        {
            CookieLogin();
            IEnumerable<Internship> model;
            HttpResponseMessage resp;
            string InternshipUrl = host + Internship_url;
            try
            {
                if (!String.IsNullOrEmpty(search))
                {
                    InternshipUrl = InternshipUrl + "?search=" + search;
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
            CookieLogin();
            Internship model;
            HttpResponseMessage resp;
            string InternshipUrl = host + Internship_url;
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
        [Route("Home/Internship/{id?}/Apply")]
        public IActionResult InternshipApply(int? id)
        {
            //CookieLogin();
            //if (_user == null)
            //{
            //    return RedirectToAction("Login", "Account", new { redirect = "Home/Internship/" + id + "/Apply" });
            //    //return RedirectToAction("Login?redirectUrl=Home/Internship/"+id+"}/Apply", "Account");
            //}
            //if(_user.Role.RoleName != "student")
            //{
            //    // To-Do Display message -> Your role does not suit the action.
            //    return Unauthorized();
            //}
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                Internship intern = _context.Internships.Find(id);
                ViewData["intern"] = intern;
                return View();
            }
        }
        [Route("Home/Internship/{id?}/Apply")]
        [HttpPost]
        public IActionResult InternshipApply(int? id, ApplyInternship fromData)
        {
            // the User is student
            // Make changes to AppliedInternship table.
            //  make nortfication.
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                string FinalCVPath;
                string FinalCLPath;
                string FinalCLString = null;
                // CV
                if (fromData.TemporaryCV != null && fromData.TemporaryCV.Length > 0)
                {
                    string UserCVFolder = _env.WebRootPath + @"\uploads\UserCV\";
                    // File of code need to be Tested
                    FinalCVPath = HelpersFunctions.StoreFile(UserCVFolder, fromData.TemporaryCV);
                }
                else
                {
                    if (fromData.isCVExisting)
                    {
                        UserDocument Doc = _context.UserDocuments.Include(u => u.User).FirstOrDefault(p =>
                        p.User.UserId == _user.UserId && p.DocumentType == "CV");
                        FinalCVPath = Doc.DocumentPath;
                    }
                    else
                    {
                        FinalCVPath = null;
                    }
                }
                // COVER Letter
                if (fromData.TemporaryCL != null && fromData.TemporaryCL.Length > 0)
                {
                    string UserCLFolder = _env.WebRootPath + @"\uploads\UserCL\";
                    // File of code need to be Tested
                    FinalCLPath = HelpersFunctions.StoreFile(UserCLFolder, fromData.TemporaryCL);
                }
                else
                {
                    if (fromData.isCLExisting)
                    {
                        UserDocument Doc = _context.UserDocuments.Include(u => u.User).FirstOrDefault(p =>
                        p.User.UserId == _user.UserId && p.DocumentType == "CL");
                        FinalCLPath = Doc.DocumentPath;
                    }
                    else
                    {
                        FinalCLPath = null;
                        if (fromData.isCLTextArea)
                        {
                            FinalCLString = fromData.WrittenCL;
                        }
                        else
                        {
                            FinalCLString = null;
                        }
                    }

                }
                Internship intern = _context.Internships.Find(id);
                // AppliedInternship constructor takes User and Internship object to create AppliedInternship object
                AppliedInternship APP_Intern = new AppliedInternship(_user, intern)
                {
                    TempCVPath = FinalCVPath,
                    TempCLPath = FinalCLPath,
                    CoverLetterText = FinalCLString,
                    EmployerStatus = "Pending"
                };
                // Adding who applied the intership
                _context.AppliedInternships.Add(APP_Intern);
                _context.SaveChanges();
                return View();
            }
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public bool IsPostBack()
        {
            return false;

        }

        public void CookieLogin() {
            // Check if Cookie Exists and if true create a Session
            // Check if Cookie Exists and if true create a Session
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["UserToken"];
            if (cookie != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserToken", cookie);
                if (_customAuthManager.Tokens.Count != 0)
                {
                    int userID = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == cookie).Value.Item3;
                    using (GlobalDBContext _context = new GlobalDBContext())
                    {
                        _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userID);
                    }
                }
            }
            else
            {
                string tokenFromSession = Response.HttpContext.Session.GetString("UserToken");
                if (tokenFromSession != null)
                {
                    int userID = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == tokenFromSession).Value.Item3;
                    using (GlobalDBContext _context = new GlobalDBContext())
                    {
                        _user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userID);
                    }
                }
            }
        }
    }

    internal class ErrorViewModel
    {
        public string RequestId { get; set; }
    }
}
