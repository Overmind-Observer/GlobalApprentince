using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Global_Intern.Models;
using Global_Intern.Data;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http; // for -> IHttpContextAccessor
using System.Net.Http; // for -> HttpClient to make request to API

namespace Global_Intern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // MENUAL ENTRY for ROLE

            using (GlobalDBContext _context = new GlobalDBContext())
            {

                if (_context.Roles.ToList().Count != 0)
                {
                    return View();
                }
                List<string> roles = new List<string>(3);
                roles.Add("Student");
                roles.Add("Employer");
                roles.Add("Teacher");
                foreach (var role in roles)
                {
                    Role r = new Role();
                    r.RoleName = role.ToLower();
                    _context.Roles.Add(r);
                    _context.SaveChanges();
                }
            }
            // ENDS
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
                model = data[0]["data"].ToObject<IEnumerable<Internship>>();
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
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    internal class ErrorViewModel
    {
        public string RequestId { get; set; }
    }
}
