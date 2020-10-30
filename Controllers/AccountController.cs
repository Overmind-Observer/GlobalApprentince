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
using Global_Intern.Models.GeneralProfile;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Global_Intern.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmailSettings _emailSettings;
        private readonly ICustomAuthManager _auth;
        IWebHostEnvironment _env;
        string host;
        IHttpContextAccessor _httpContextAccessor;

        public AccountController(IOptions<EmailSettings> emailSetting, ICustomAuthManager auth,
            IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _emailSettings = emailSetting.Value;
            _auth = auth;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;
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
                User user = _context.Users.FirstOrDefault(u => u.UserEmail == new_user.Email);
                if (user != null)
                {
                    ViewBag.Message = new_user.FirstName + " " + new_user.LastName + " successfully registered. A Email has been sent for the verfication.";
                    return View();
                }

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
                string uniqueToken = Guid.NewGuid().ToString("N").Substring(0, 6);
                theUser.UniqueToken = uniqueToken;
                
                _context.Users.Add(theUser);
                
                SendEmail email = new SendEmail(_emailSettings);
                string fullname = theUser.UserFirstName + " " + theUser.UserLastName;
                string msg = "Please verify you email account for the verification. Click on the link to verify :";
                msg += _domainurl + "/Account/ConfirmEmail?email=" + theUser.UserEmail + "&token=" + theUser.UniqueToken;
                
                
                _context.SaveChanges();
                email.SendEmailtoUser(fullname, theUser.UserEmail, "Email Verification", msg);
                ViewBag.Messsage = new_user.FirstName + " " + new_user.LastName + " successfully registered. A Email has been sent for the verfication.";
            }
            return View();
        }

        public IActionResult Login([FromQuery] string redirect)
        {
            if (redirect != null)
            {
                TempData["redirect"] = redirect;
                
            }
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
                    //Check email is verified
                    if(theUser.UserEmailVerified == false)
                    {
                        ModelState.AddModelError("", "Email is not verifed you cant login.");
                        return View();
                    }
                    CustomPasswordHasher pwd = new CustomPasswordHasher();
                    string hashed = pwd.HashPassword(user.Password, theUser.Salt);
                    // Check if the user entered password is correct
                    if (hashed == theUser.UserPassword)
                    {
                        
                        //string usr = JsonConvert.SerializeObject(theUser, Formatting.Indented, new JsonSerializerSettings()
                        //{
                        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        //});

                        // Custom Auth Token
                        
                        var token = _auth.Authenticate(theUser.UserEmail, theUser.Role.RoleName, theUser.UserId);
                        // Create Sessions
                        //HttpContext.Session.SetString("UserSession", usr);
                        HttpContext.Session.SetString("UserToken", token);

                        // Delete Existing cookie
                        Response.Cookies.Delete("UserToken");
                        //Create Cookie
                        if (user.RememberMe)
                        {
                            CookieOptions cookieOptions = new CookieOptions();
                            cookieOptions.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Append("UserToken", token, cookieOptions);
                        }

                        // at Genearted token in header
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        }
                        if (TempData.ContainsKey("redirect"))
                        {
                            string redirectUrl = TempData["redirect"].ToString();
                            string fullPath = host + "/" + redirectUrl;
                            return Redirect(fullPath);
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
                        if (theUser.Role.RoleId == 3)
                        {
                            // Teacher
                            return RedirectToAction("Index", "DashboardTeacher");
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
        
        public IActionResult Logout()
        {

            // This Remove token from Authmanager
            string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
            _auth.removeToken(token);
            Response.Cookies.Delete("UserToken");

            return RedirectToAction("Login");
        }
        public IActionResult ConfirmEmail(string email, string token)
        {
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                // Recuqire Url encode - decode
                string encoded = System.Net.WebUtility.UrlEncode(token);
                // prevent cross site scripting.
                // Check given Email and salt(token) are in the same user 
                User theUser = _context.Users.Include(r=>r.Role).Where(u => u.UserEmail == email).FirstOrDefault<User>();
                // if we found the user
                if (theUser.UniqueToken == token)
                {
                    // update the EmailVerified to True in the User table
                    theUser.UserEmailVerified = true;
                    _context.Users.Update(theUser);
                    _context.SaveChanges();
                    TempData["compeleteProfileUserId"] = JsonConvert.SerializeObject(theUser.UserId);
                    ViewBag.message = theUser.UserEmail + " is Verifed. Now your can login to our site.";
                    // Uncommnet below line to
                    // login user came via email link.
                    //_auth.Authenticate(theUser.UserEmail, theUser.Role.RoleName, theUser.UserId);
                    return View();
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        
        // new add for Forget Password 2020-09-09. 
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        


        
    }
}
