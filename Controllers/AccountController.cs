using System;
using System.Linq;
using Global_Intern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Global_Intern.Util;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Register(AccountRegister new_user)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                // ->TODO Validation check on clinet side using Jquery or JavaScript

                // Password hashed with extra layer (salt) of security
                string password = new_user.Password;
                CustomPasswordHasher pwd = new CustomPasswordHasher();
                // increse the size to increase secuirty but lower performance 
                string salt = pwd.CreateSalt(10);
                string hashed = pwd.HashPassword(password, salt);
                //new_user.Salt = salt;
                new_user.Password = hashed;
                // var errors = ModelState.Values.SelectMany(v => v.Errors);
                Role role = _context.Roles.Find(new_user.UserRole);
                User theUser = new User();
                theUser.AddFromAccountRegsiter(new_user, role, salt); 

                _context.Users.Add(theUser);
                _context.SaveChanges();
                ViewBag.Messsage = new_user.FirstName + " " + new_user.LastName + " successfully registered.";
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLogin user)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                //var userA = _context.Users.ToList<User>();
                User theUser = _context.Users.Include(p => p.Role).FirstOrDefault(u => u.UserEmail == user.Email);
                // Check if the user with email exists
                if (theUser != null)
                {
                    CustomPasswordHasher pwd = new CustomPasswordHasher();
                    string hashed = pwd.HashPassword(user.Password, theUser.Salt);
                    // Check if the user entered password is correct
                    if (hashed == theUser.UserPassword)
                    {
                        HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(theUser.UserEmail));
                        // Id 1 for Student & Id 2 for Employer
                        if (theUser.Role.RoleId == 1)
                        {
                            // Student
                            return RedirectToAction("Index", "DashboardStudent");
                        }
                        if (theUser.Role.RoleId == 2)
                        {
                            // Employer
                            return RedirectToAction("Index", "DashboardEmployer");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Credentials.");
                    }
                }
                else {
                    ModelState.AddModelError("", "No user exists with the given email.");
                    ModelState.AddModelError("", "Wrong Credentials.");
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