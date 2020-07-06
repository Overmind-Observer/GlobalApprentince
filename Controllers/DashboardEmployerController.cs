using System;
using Global_Intern.Models;
using Global_Intern.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Global_Intern.Controllers
{

    public class DashboardEmployerController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;
        private readonly string host; 
        public DashboardEmployerController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth)
        {
            _httpContextAccessor = httpContextAccessor;
            host = _httpContextAccessor.HttpContext.Request.Host.Value;
            _customAuthManager = auth;
        }

        [Authorize(Roles = "employer")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Internships()
        {
            return View();
        }

    }
}