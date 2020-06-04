using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Global_Intern.Controllers
{
    public class DashboardStudentController : Controller
    {
        public DashboardStudentController()
        {
            
            if (HttpContext.Session.GetString("UserSession") == null)
            {

                Console.WriteLine("Not authorized Require Login");
                RedirectToAction("Index", "Account");

            }
            else
            {
                JObject user = JObject.Parse(HttpContext.Session.GetString("UserSession"));
                // TODO -> CHECK THE ROLE IS Student
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}