using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Global_Intern.Models;
using static System.Linq.Enumerable;


namespace Global_Intern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // MENUAL ENTRY for ROLE
            
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                if (_context.Roles.ToList().Count != 0)
                {
                    return View();
                }
                List<string> roles = new List<string>(3);
                roles.Add("Student");
                roles.Add("Employer");
                roles.Add("Teacher");
                foreach (var role in roles)
                {
                    Role r = new Role();
                    r.RoleName = role.ToLower();
                    _context.Roles.Add(r);
                    _context.SaveChanges();
                }
            }
            // ENDS
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    internal class ErrorViewModel
    {
        public string RequestId { get; set; }
    }
}
