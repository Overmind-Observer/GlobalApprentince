using System.Collections.Generic;
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
        private User LoggedIn_User = null;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth)
        {
            _customAuthManager = auth;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            CookieLogin();
            
            // For NavBar to display user is LoggedIn
            ViewData["LoggeduserName"] = LoggedIn_User != null? LoggedIn_User.UserFirstName + ' ' + LoggedIn_User.UserLastName : null;
            
            return View();
        }

        public async Task<IActionResult> AllInternships([FromQuery]string search, int pageNumber = 0, int pageSize = 0)
        {
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

                // Display User is LoggedIn
                ViewData["LoggeduserName"] = LoggedIn_User != null ? LoggedIn_User.UserFirstName + ' ' + LoggedIn_User.UserLastName : null;

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public async Task<IActionResult> Internship(int id)
        {
            Internship model;
            HttpResponseMessage resp;
            string InternshipUrl = host + Internship_url;

            // Display User is LoggedIn
            ViewData["LoggeduserName"] = LoggedIn_User != null ? LoggedIn_User.UserFirstName + ' ' + LoggedIn_User.UserLastName : null;

            try
            {

                resp = await _client.GetAsync(InternshipUrl + "/" + id.ToString()) ;
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
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                Internship intern = _context.Internships.Find(id);
            }
                return View();
        }
        [Route("Home/Internship/{id?}/Apply")]
        [HttpPost]
        public IActionResult InternshipApply(int? id, ApplyInternship fromData)
        {
            // the User is student

            // Make changes to AppliedInternship table.
            // make nortfication.
            try
            {
                using (GlobalDBContext _context = new GlobalDBContext())
                {
                    Internship intern = _context.Internships.Find(id);
                    User user = _context.Users.Find(_customAuthManager.Tokens.FirstOrDefault().Value.Item3);
                    AppliedInternship APP_Intern = new AppliedInternship(user, intern);
                    _context.AppliedInternships.Add(APP_Intern);
                    _context.SaveChanges();
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
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
           var cookie = Request.Cookies["UserToken"];
            if (cookie != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserToken", cookie);
                if (_customAuthManager.Tokens.Count != 0)
                {
                    int userID = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == cookie).Value.Item3;
                    using (GlobalDBContext _context = new GlobalDBContext())
                    {
                        LoggedIn_User = _context.Users.Find(userID);
                    }
                }
            }
            else {
                string tokenFromSession = Response.HttpContext.Session.GetString("UserToken");
                if (tokenFromSession != null) {
                    int userID = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == tokenFromSession).Value.Item3;
                    using (GlobalDBContext _context = new GlobalDBContext())
                    {
                        LoggedIn_User = _context.Users.Find(userID);
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
