using System;
using System.Linq;
using Global_Intern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Global_Intern.Util;
using Microsoft.EntityFrameworkCore;
using Global_Intern.Services;
using Global_Intern.Data;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Global_Intern.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmailSettings _emailSettings;
        private readonly ICustomAuthManager _auth;

        public AccountController(IOptions<EmailSettings> emailSetting, ICustomAuthManager auth)
        {
            _emailSettings = emailSetting.Value;
            _auth = auth;
        }

        
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
                string _domainurl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                // ->TODO Validation check on clinet side using Jquery or JavaScript

                // Password hashed with extra layer of security
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
                SendEmail email = new SendEmail(_emailSettings);
                string fullname = theUser.UserFirstName + " " + theUser.UserLastName;
                string msg = "Please verify you email account for the verification. Click on the link to verify :";
                msg += _domainurl + "/Account/ConfirmEmail?email=" + theUser.UserEmail + "&token=" + salt;
                email.SendEmailtoUser(fullname, theUser.UserEmail, "Email Verification", msg);
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
                        string usr = JsonConvert.SerializeObject(theUser, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                        // Custom Auth Token
                        var token = _auth.Authenticate(theUser.UserEmail, theUser.Role.RoleName, theUser.UserId);
                        // set Sessions
                        HttpContext.Session.SetString("UserSession", usr);
                        HttpContext.Session.SetString("UserToken", "Bearer " + token);

                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        }

                        //Request.Headers.Add()
                        if (token == null)
                        {
                            return Unauthorized();
                        }

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

        public string ConfirmEmail(string email, string token)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {   
                // Check given Email and salt(token) are in the same user 
                User theUser = _context.Users.FirstOrDefault(u => u.UserEmail == email && u.Salt == token);
                // if we found the user
                if (theUser != null)
                {
                    // update the EmailVerified to True in the User table
                    theUser.UserEmailVerified = true;
                    _context.Users.Update(theUser);
                    _context.SaveChanges();
                    return theUser.UserEmail + "is Verifed. Login to our site.";
                }
                else
                {
                    return "Link Expired";
                }
            }
        }
        
    }
}