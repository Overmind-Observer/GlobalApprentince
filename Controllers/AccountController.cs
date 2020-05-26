using System;
using System.Linq;
using Global_Intern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Global_Intern.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            // Password hashed with extra layer (salt) of security
            string password = user.UserPassword;
            user.UserPassword = passwordHasher(password);

            if (ModelState.IsValid)
            {
                using (GlobalDBContext _context = new GlobalDBContext()) {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
            }
            ModelState.Clear();
            ViewBag.Messsage = user.UserFirstName + " " + user.UserLastName + " successfully registered.";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {

                var theUser = _context.Users.Single(u => u.UserEmail == user.UserEmail && u.UserPassword == user.UserPassword);
                if (theUser != null)
                {
                    HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(theUser));
                    return RedirectToAction("Index", "DashboardEmployer");
                }
                else {
                    ModelState.AddModelError("", "Email or Password is wrong.");
                }
                
                return View();
            }
        }


        public string passwordHasher(string password) {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        } 
    }
}