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

namespace Global_Intern.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmailSettings _emailSettings;
        private readonly ICustomAuthManager _auth;
        IWebHostEnvironment _env;

        public AccountController(IOptions<EmailSettings> emailSetting, ICustomAuthManager auth, IWebHostEnvironment env)
        {
            _emailSettings = emailSetting.Value;
            _auth = auth;
            _env = env;
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
                string uniqueToken = Guid.NewGuid().ToString("N").Substring(0, 6);
                theUser.UniqueToken = uniqueToken;
                _context.Users.Add(theUser);
                
                SendEmail email = new SendEmail(_emailSettings);
                string fullname = theUser.UserFirstName + " " + theUser.UserLastName;
                string msg = "Please verify you email account for the verification. Click on the link to verify :";
                msg += _domainurl + "/Account/ConfirmEmail?email=" + theUser.UserEmail + "&token=" + uniqueToken;
                email.SendEmailtoUser(fullname, theUser.UserEmail, "Email Verification", msg);
                _context.SaveChanges();
                ViewBag.Messsage = new_user.FirstName + " " + new_user.LastName + " successfully registered. A Email has been sent for the verfication.";
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
                        


                        string usr = JsonConvert.SerializeObject(theUser, Formatting.Indented, new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                        // Custom Auth Token
                        
                        var token = _auth.Authenticate(theUser.UserEmail, theUser.Role.RoleName, theUser.UserId);
                        // set Sessions
                        HttpContext.Session.SetString("UserSession", usr);
                        HttpContext.Session.SetString("UserToken", "Bearer " + token);

                        // at Genearted token in header
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
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
        [HttpGet]
        public IActionResult GeneralProfile()
        {
            // action when email is verified.

            
            //if(_auth.Tokens.Count == 0)
            //{
            //    return Unauthorized();
            //}

            //ViewBag.Message = TempData["message"];
            using (GlobalDBContext _context = new GlobalDBContext())
            {
                //int userID = _auth.Tokens.FirstOrDefault().Value.Item3;

                User user = _context.Users.Include(p => p.Role).SingleOrDefault(x => x.UserId == 2);
                GeneralProfile gen = new GeneralProfile(user);
                return View(gen);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> UploadUserImage(IFormFile UserImage)
        {
            if(UserImage != null && UserImage.Length > 0)
            {
                var imagePath = @"\uploads\UserImage\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var uniqueFileName = Guid.NewGuid().ToString();
                var fileName = Path.GetFileName(uniqueFileName + "." + UserImage.FileName.Split(".")[1].ToLower());
                string fullPath = uploadPath + fileName;

                imagePath = imagePath + @"\";
                var filePath = @".." + Path.Combine(imagePath, fileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await UserImage.CopyToAsync(fileStream);
                }
                ViewData["FileLocation"] = filePath;
                using (GlobalDBContext _context = new GlobalDBContext()) {
                    User user = _context.Users.Find(2);
                    user.UserImage = filePath;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("GeneralProfile");
        }

        [HttpPost]
        public IActionResult GeneralProfile(GeneralProfile generalProfile) {
            if (generalProfile.UserImage != null && generalProfile.UserImage.Length > 0) {
                //var imagePath = @"\uploads\UserImage\";
                //var uploadPath = _env.WebRootPath + imagePath;
                string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + generalProfile.UserImage.FileName;
                string filePath = uploadFolder + uniqueFileName;
                generalProfile.UserImage.CopyTo(new FileStream(filePath, FileMode.Create));
                using (GlobalDBContext _context = new GlobalDBContext())
                {
                    //User user = _context.Users.Find(_auth.Tokens.FirstOrDefault().Value.Item3);
                    User user = _context.Users.Find(2);
                    user.AddFromAccountGeneralProfile(generalProfile, uniqueFileName);
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    GeneralProfile gen = new GeneralProfile(user);
                    return View(gen);
                }
            }
            return View();
            
        }
        public IActionResult Logout()
        {
            string GUIDtoken = _auth.Tokens.FirstOrDefault().Key;
            _auth.removeToken(GUIDtoken);

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
                    TempData["message"] = theUser.UserEmail + "is Verifed. Login to our site.";
                    _auth.Authenticate(theUser.UserEmail, theUser.Role.RoleName, theUser.UserId);
                    return RedirectToAction("GeneralProfile");
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        


        
    }
}