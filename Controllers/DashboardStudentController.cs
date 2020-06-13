using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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