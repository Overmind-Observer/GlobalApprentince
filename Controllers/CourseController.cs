using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Util;
using Global_Intern.Util.pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Global_Intern.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly GlobalDBContext _context;
        private readonly string _table;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;

        public CourseController(GlobalDBContext context,
            IHttpContextAccessor httpContextAccessor,
            ICustomAuthManager auth)
        {
            _customAuthManager = auth;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _table = "Course";
        }

        // GET: api/Course
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses([FromQuery] string search, int pagenumber = 1, int pagesize = 10)
        {
            List<Course> courses;

            if (!String.IsNullOrEmpty(search))
            {
                string query = "SELECT * FROM " + _table + "WHERE(CourseTitle LIKE('%" + search + "%') OR CourseType LIKE('%" + search + "%') OR CourseInfo LIKE('%" + search + "%'))";
                courses = _context.Course.FromSqlRaw(query).Include(u => u.User).OrderBy(x => x.CourseExpDate).ToList();
            }
            else
            {
                courses = _context.Course.Include(u => u.User).OrderBy(p => p.CourseCreatedAt).ToList();
                //courses = _context.Internships.Include(u => u.User).OrderBy(x => x.InternshipCreatedAt).ToList()
            }

            //var filtered = UserFilter.RemoveUserInfoFromCourses(courses);

            //filtered = _context.Course.OrderBy(j => j.CourseExpDate).ToList();

            var response = PaginationQuery<Course>.CreateAsync(courses, pagenumber, pagesize);

            return Ok(response);
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
