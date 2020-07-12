using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Global_Intern.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Global_Intern.Controllers
{

    public class DashboardStudentController : Controller
    {
        private readonly User _logedInUser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardStudentController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize(Roles = "student")]
        public IActionResult Index()
        {
            
            return View();
        }


        //public IActionResult Authorization(bool _SessionUser)
        //{
        //    if (_SessionUser)
        //    {
        //        if(_logedInUser.Role.RoleId == 2)
        //        {
        //            return RedirectToAction("Index", "DashboardEmployer");
        //        }
        //        return RedirectToAction("Index", "DashboardTeacher");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //}
    }
}