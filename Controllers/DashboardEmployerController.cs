using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Global_Intern.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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