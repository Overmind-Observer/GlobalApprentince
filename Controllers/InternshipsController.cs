using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Util;
using Global_Intern.Util.pagination;
using Global_Intern.Models.Filters;
using Global_Intern.Models.EmployerModels;

namespace Global_Intern.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipsController : ControllerBase
    {
        private readonly GlobalDBContext _context;
        private readonly string _table;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthManager _customAuthManager;


        public InternshipsController(GlobalDBContext context, 
            IHttpContextAccessor httpContextAccessor,
            ICustomAuthManager auth)
        {
            _customAuthManager = auth;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _table = "Internships";
        }

        // GET: api/Internships
        [HttpGet]
        public ActionResult<IEnumerable<Internship>> GetInternships([FromQuery]string search, int pageNumber = 1, int PageSize = 10)
        {
            List<Internship> interns;

            if (!String.IsNullOrEmpty(search))
            {
                string sql = "SELECT * FROM " + _table + " WHERE (InternshipBody like('%" + search + "%') or InternshipType like ('%" + search + "%') or InternshipTitle like('%" + search + "%'))";
                interns =  _context.Internships.FromSqlRaw(sql).Include(u => u.User).OrderBy(x => x.InternshipExpDate).ToList();
            }
            else
            {
                interns = _context.Internships.Include(u => u.User).OrderBy(x => x.InternshipCreatedAt).ToList();
            }
            
            // Make sensitive info like salt and password null or empty
            var filtered = UserFilter.RemoveUserInfoFromInternship(interns);
            // the Response class will shows if the data is paginated or require token (Auth).
            
            var response = PaginationQuery<Internship>.CreateAsync(filtered, pageNumber, PageSize);
            

            return Ok(response);
        }

        // GET: api/Internships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Internship>> GetInternship(int id)
        {
            var internship = await _context.Internships.FindAsync(id);

            if (internship == null)
            {
                return NotFound();
            }

            return internship;
        }

        // PUT: api/Internships/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInternship(int id, InternshipModel internshipModel)
        {
            //if (id != internship.InternshipId)
            //{
            //    return BadRequest();
            //}
            try
            {
                var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

                // ToDO-> get User ID from Session
                User user = _context.Users.Find(User_id);
                Internship internship = new Internship();
                // SetAddorUpdateIntern(Intership - TYPE, User =TYPE, Bool -TYPE)
                // the above method fill the object with user provided values and bool if it is for update
                internship.SetAddorUpdateIntern(internshipModel, user, true, id);
                _context.Internships.Update(internship);
                _context.SaveChanges();
                return Ok(internship);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Internships
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [ApiKeyAuth]
        [HttpPost]
        public ActionResult<Internship> PostInternship(InternshipModel newInternship)
        {
            try
            {
                var id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;
                
                // ToDO-> get User ID from Session
                User user = _context.Users.Find(id);
                Internship internship = new Internship();
                // SetAddorUpdateIntern(Intership - TYPE, User =TYPE, Bool -TYPE)
                // the above method fill the object with user provided values and bool if it is for update
                internship.SetAddorUpdateIntern(newInternship, user);
                _context.Internships.Add(internship);
                _context.SaveChanges();
                return Ok(internship);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Internships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Internship>> DeleteInternship(int id)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
            {
                return NotFound();
            }

            _context.Internships.Remove(internship);
            await _context.SaveChangesAsync();

            return internship;
        }

        private bool InternshipExists(int id)
        {
            return _context.Internships.Any(e => e.InternshipId == id);
        }
    }
}
