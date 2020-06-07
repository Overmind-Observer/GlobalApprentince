using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Global_Intern.Controllers
{
    public class DashboardEmployerController : Controller
    {
        public DashboardEmployerController()
        {
            var user = HttpContext.Session.GetString("UserSession");
            if (user == null){

                Console.WriteLine("Not authorized Require Login");
                RedirectToAction("Index", "Account");
                
            }
            else
            {
                
                // TODO -> CHECK THE ROLE IS Employer
            }
        }
        
        



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}