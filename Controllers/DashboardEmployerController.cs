using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Util;
using Global_Intern.Util.pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Global_Intern.Controllers
{

    public class DashboardEmployerController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host;
        private readonly HttpClient _client = new HttpClient();
        private readonly string Internship_url = "/api/Internships";
        public DashboardEmployerController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth)
        {
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;
            //https://localhost:44307/api/internships/employer/3

        }

        //[Authorize(Roles = "employer")]
        public IActionResult Index()
        {
            using(GlobalDBContext _context = new GlobalDBContext())
            {
                //var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;
                User user = _context.Users.Find(3);
                ViewBag.IntershipsByLoginedInUser = _context.Internships.Where(e => e.User == user).ToList();
            }
                return View();
        }

        public async Task<IActionResult> InternshipsAsync([FromQuery]string search, int pageNumber = 0, int pageSize = 0)
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
            }
            catch (Exception)
            {
                throw;
            }

            return View(model);
        }

    }
}