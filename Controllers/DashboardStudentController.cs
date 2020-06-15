using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Session;
using Newtonsoft.Json.Linq;
using System.Web;
using Global_Intern.Models;
using Newtonsoft.Json;


namespace Global_Intern.Controllers
{
    public class DashboardStudentController : Controller
    {
        private readonly User _logedInUser;

        public DashboardStudentController(IHttpContextAccessor httpContextAccessor)
        {

            try
            {
                _logedInUser = JsonConvert.DeserializeObject<User>(httpContextAccessor.HttpContext.Session.GetString("UserSession"));
                Authorization(true);
            }
            catch (Exception e)
            {
                Authorization(false);

            var usr = HttpContext.Session.GetString("UserSession");

            if(usr != null)
            {


            }
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Authorization(bool _SessionUser)
        {
            if (_SessionUser)
            {
                if(_logedInUser.Role.RoleId == 2)
                {
                    return RedirectToAction("Index", "DashboardEmployer");
                }
                return RedirectToAction("Index", "DashboardTeacher");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}