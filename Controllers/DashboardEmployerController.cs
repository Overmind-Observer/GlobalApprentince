using System;
using Global_Intern.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Global_Intern.Controllers
{

    public class DashboardEmployerController : Controller
    {
        public DashboardEmployerController()
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
            if (user == null){

                Console.WriteLine("Not authorized Require Login");
                RedirectToAction("Index", "Account");
            }
            else
            {
                
                // TODO -> CHECK THE ROLE IS Employer
            }
        }

        [Authorize(Roles = "employer")]
        public IActionResult Index()
        {
            return View();
        }

    }
}