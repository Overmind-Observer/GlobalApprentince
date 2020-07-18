using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Global_Intern.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Global_Intern.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        //[Authorize(Roles = "student")]
        public IActionResult Index()
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                //var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;
                User user = _context.Users.Find(2);
                var appliedInterns = _context.AppliedInternships.Include(i => i.Internship).Where(e => e.User == user).ToList();
                List<Internship> interns = new List<Internship>();
                foreach(var appliedIntern in appliedInterns)
                {
                    Internship theIntern = appliedIntern.Internship;
                    interns.Add(theIntern);
                }
            }
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