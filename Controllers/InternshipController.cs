using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Global_Intern.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Global_Intern.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Global_Intern.Controllers
{
    public class InternshipController : Controller
    {
        // GET: Internship
        private readonly GlobalDBContext _context;
        private readonly string _table;
        public InternshipController() {
            _context = new GlobalDBContext();
            _table = "Internships";
        }
        public ActionResult Index(string search, bool isPaid)
        {
            if (String.IsNullOrEmpty(search))
            {
                return View(_context.Internships.ToList<Internship>());
            }
            string sql = "SELECT * FROM " + _table + " WHERE (InternshipBody like('%" + search + "%') or InternshipType like ('%" + search + "%') or InternshipTitle like('%" + search + "%'))";
            return View(_context.Internships.FromSqlRaw(sql).ToList());
            
        }
        [HttpPost]
        public ActionResult Index(IntershipFilter filter)
        {
            //ViewBag.NameSortParm = String.IsNullOrEmpty(filter) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            
           
            return View();

            //return View(_context.Internships.ToList<Internship>());
        }

        // GET: Internship/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Internship/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Internship/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Internship intern)
        {
            
            try
            {
                // ToDO-> get User ID from Session
                // geting menual user
                User user = _context.Users.Find(2);
                Internship internship = new Internship();
                // SetAddorUpdateIntern(Intership - TYPE, User =TYPE, Bool -TYPE)
                // the above method fill the object with user provided values and bool if it is for update
                internship.SetAddorUpdateIntern(intern, user);
                _context.Internships.Add(internship);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Internship/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Internship/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Internship/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Internship/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}