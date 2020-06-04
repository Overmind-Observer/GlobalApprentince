using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Global_Intern.Models;

namespace Global_Intern.Controllers
{
    public class InternshipController : Controller
    {
        // GET: Internship
        private readonly GlobalDBContext _context;
        public InternshipController() {
            _context = new GlobalDBContext();
        }
        public ActionResult Index()
        {
            var interns = _context.Internships.ToList<Internship>();
            ViewData["msg"] = "This is Intership";
            return View(interns);
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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