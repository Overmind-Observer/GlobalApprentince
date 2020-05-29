using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Global_Intern.Controllers
{
    public class DashboardEmployerController : Controller
    {
        public DashboardEmployerController()
        {
            if (HttpContext.Session.GetString("UserSession") == null){

                Console.WriteLine("Not authorized Require Login");
                RedirectToAction("Index", "Account");
                
            }
            else
            {
                JObject user = JObject.Parse(HttpContext.Session.GetString("UserSession"));
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