using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json.Linq;
using System.Web;

namespace Global_Intern.Controllers
{
    public class DashboardStudentController : Controller
    {
        public DashboardStudentController()
        {
            var usr = HttpContext.Session.GetString("UserSession");
            if(usr != null)
            {
                
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}