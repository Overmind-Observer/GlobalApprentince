using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Models.GeneralProfile;
using Global_Intern.Models.StudentModels;
using Global_Intern.Util;
using Global_Intern.Util.pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Global_Intern.Controllers
{
	[Authorize(Roles = "student")]
	public class DashboardStudentController : Controller
	{
		////https://localhost:44307/api/internships/employer/3

		private readonly IHttpContextAccessor _httpContextAccessor; // Accessor allows access to session and cookies - System defined instance
		private readonly ICustomAuthManager _customAuthManager; // User defined instance ( Created by Developer )
		private readonly string Internship_url = "/api/Internships";
		private readonly GlobalDBContext _context;
		private readonly string _table;
		string Qualification_url = "/api/Qualifications";
		private readonly string host;
		StudentInternProfile studentIntern = null;
		GlobalDBContext context = new GlobalDBContext();
		int QualificationId;
		int ExperienceId;


		private readonly HttpClient _client = new HttpClient(); // Used to access API -> Internship.
		//private readonly string Internship_url = "/api/Internships"; - does not used!?
		IWebHostEnvironment _env; // to access the Content PATH aka wwwroot
		/// <summary>
		/// User object is quite important here. without accessing database again and again on every action. User is set on constructor level.
		/// </summary>
		private User _user;
		public DashboardStudentController(IHttpContextAccessor httpContextAccessor, ICustomAuthManager auth, IWebHostEnvironment env, GlobalDBContext context)
		{
			_env = env;

			// gets Session or host name 
			_httpContextAccessor = httpContextAccessor;
			host = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value;

			// To Access runtime tokens
			_customAuthManager = auth;

			setUser();
		}

		public void DashboardOptions()
		{
			// Display User name on the right-top corner - shows user is logedIN
			ViewData["LoggeduserName"] = new List<string>() { _user.UserFirstName + ' ' + _user.UserLastName, _user.UserImage };

			// Geting Dashboard Menu from project/data/DashboardMenuOption.json into ViewData
			string path = _env.ContentRootPath + @"\Data\DashboardMenuOptions.json";
			ViewData["menuItems"] = HelpersFunctions.GetMenuOptionsForUser(_user.UserId, path);
		}
		public IActionResult Index()
		{
			DashboardOptions();

			using (GlobalDBContext _context = new GlobalDBContext())
			{
				// Geting internshps student applied for using his/her userID
				var appliedInterns = _context.AppliedInternships.Include(i => i.Internship).Where(e => e.User == _user).ToList();
				List<Internship> interns = new List<Internship>();
				foreach (var appliedIntern in appliedInterns)
				{
					Internship theIntern = appliedIntern.Internship;
					interns.Add(theIntern);
				}
				ViewBag.IntershipsByLoginedInUser = _context.Internships.Where(e => e.User == _user).ToList();
			}
			return View();
		}


		//[Authorize]
		[HttpGet]
		public IActionResult GeneralProfile()
		{
			DashboardOptions();

			using GlobalDBContext context = new GlobalDBContext();
			
			var temp = context.StudentInternProfiles.Where(u=>u.User==_user).ToList();

			ConsoleLogs logs = new ConsoleLogs(_env);

			logs.WriteDebugLog(temp.Count().ToString());



			if (temp.Count() == 0)
			{
				ProfileViewStudent userViewModel = new ProfileViewStudent(_user);

				return View(userViewModel);
			}
			else
			{
				studentIntern = temp[0];

				ProfileViewStudent userViewModel = new ProfileViewStudent(_user, studentIntern);

				return View(userViewModel);
			}



			


		 }

		[HttpPost]
		public IActionResult GeneralProfile(ProfileViewStudent updatedUser)
		{
			DashboardOptions();
			bool imageUploaded= true; ;
			StudentInternProfile internProfile = new StudentInternProfile();
			try
			{
				//if (updatedUser.UserImage!=null)
				//{
				//    if (_user.UserImage.Length > 0)
				//    {
				//        updatedUser.UserImageName = _user.UserImage;
				//    }
				//}
			}catch(Exception e)
			{
				//throw;
				imageUploaded = false;
				ConsoleLogs consoleLogs = new ConsoleLogs(_env);

				consoleLogs.WriteDebugLog(e.Message.ToString());
				consoleLogs.WriteDebugLog(e.StackTrace);
				consoleLogs.WriteDebugLog(e.Source);
				consoleLogs.WriteDebugLog(e.TargetSite.ToString());
				consoleLogs.WriteDebugLog(e.InnerException.ToString());

			}
			//-------------------- END
			using (GlobalDBContext _context = new GlobalDBContext())
			{
				if (updatedUser.UserImage != null && updatedUser.UserImage.Length > 0)
				{
					string uploadFolder = _env.WebRootPath + @"\uploads\UserImage\";

					// File of code need to be Tested
					//string file_Path = HelpersFunctions.StoreFile(uploadFolder, generalProfile.UserImage);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + updatedUser.UserImage.FileName;
					// Delete previous uploaded Image
					if (!String.IsNullOrEmpty(updatedUser.UserImage.ToString()))
					{
						string imagePath = uploadFolder + _user.UserImage;
						if (System.IO.File.Exists(imagePath))
						{
							// If file found, delete it    
							System.IO.File.Delete(imagePath);
							Console.WriteLine("File deleted.");
						}
					}
					string filePath = uploadFolder + uniqueFileName;
					FileStream stream = new FileStream(filePath, FileMode.Create);
					updatedUser.UserImage.CopyTo(stream);
					stream.Dispose();
					//try
					//{
					//    if(updatedUser.UserImageName)
					//}
					updatedUser.UserImageName = uniqueFileName;

					// if new image is uploaded with other user info
									
				}

				if (updatedUser.UserImageName==null)
				{
					updatedUser.UserImageName = _user.UserImage;
				}

				_user = _user.UpdateUserStudent(_user, updatedUser);

				//DateTime date = Convert.ToDateTime(Request.Form["UserDob"]);

				//DateTime date1 = Convert.ToDateTime(Request.Form["userVisaExpiryId"]);

				ProfileViewStudent profileView = new ProfileViewStudent();

				GlobalDBContext context = new GlobalDBContext();

				var temp = context.StudentInternProfiles.Where(u => u.User == _user).ToList();


				if (temp.Count==0)
				{
					internProfile = profileView.updateOrCreateStudentInternProfile(internProfile, updatedUser, _user);
					_context.Users.Update(_user);
					_context.StudentInternProfiles.Add(internProfile);
					_context.SaveChanges();
				}
				else
				{
					internProfile = temp[0];
					internProfile = profileView.updateOrCreateStudentInternProfile(internProfile, updatedUser, _user);
					_context.Users.Update(_user);
					_context.StudentInternProfiles.Update(internProfile);
					_context.SaveChanges();
				}
				

				




				ViewBag.Message = updatedUser.UserFirstName + " " + updatedUser.UserLastName + " has been updated successfully. Check the Users table to see if it has been updated.";

				var temp1 = _context.Users.Find(_user.UserId);

				var temp2 = context.StudentInternProfiles.Where(u => u.User == _user).ToList();

				var temp3 = temp2[0];

				ProfileViewStudent userViewModel = new ProfileViewStudent(temp1,temp3);

					return View(userViewModel);
			}

		}


		public IActionResult Settings()
		{
			DashboardOptions();

			ViewBag.Message = "Are you sure you want to delete user " + _user.UserFirstName + " " + _user.UserLastName;

			ViewBag.DeleteItem = "DeleteUser";

			return View();
		}

		public IActionResult DeleteUser()
		{

			using (GlobalDBContext _context = new GlobalDBContext())
			{

				var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

				User user = _context.Users.Find(User_id);

				_context.Users.Remove(user);

				_context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

		}

		public void setUser()
		{
			///  Access "UserToken" Session. 
			/// NOTE:  Session get created when user login with unique id. This id is also used to identify the user from number of Auth Tokens
			string token = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
			if (token == null) // if null user has not loggedIn
			{
				return;
			}
			using (GlobalDBContext tooMuchConfusion = new GlobalDBContext())
			{

				if (_customAuthManager.Tokens.Count > 0)
				{
					// check weather the unique id is in AuthManager
					int userId = _customAuthManager.Tokens.FirstOrDefault(i => i.Key == token).Value.Item3;
					// User is found in the AuthManager
					_user = tooMuchConfusion.Users.Include(r => r.Role).FirstOrDefault(u => u.UserId == userId);
				}

			}
		}


		public IActionResult Qualifications()
		{
			DashboardOptions();


			using (GlobalDBContext _context = new GlobalDBContext())
			{
				// Gets all qualifications created by the user
				var qualifications = _context.Qualifications.ToList();

				return View(qualifications);
			}


		}
		//[HttpPost]
		//public IActionResult Qualifications(int id)
		//{
		//	DashboardOptions();


		//	using (GlobalDBContext _context = new GlobalDBContext())
		//	{
				
		//		Qualification qualification = context.Qualifications.Find(id);
		//		ViewBag.DeleteMessage = "Are you sure you want to delete qualification at " + qualification.QualificationSchool + "?";
		//		return View();
		//	}


		//}


		public IActionResult AddQualification()
		{
			DashboardOptions();

			return View();
		}


		[HttpPost]
		public IActionResult AddQualification(Qualification NewQualification)
		{
			DashboardOptions();

			var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

			using (GlobalDBContext _context = new GlobalDBContext())
			{

				Qualification nQualification = new Qualification();
				User user = _context.Users.Find(User_id);

				// This creates the new qualification and updates the database
				nQualification.AddNewQualification(NewQualification, user);
				_context.Qualifications.Add(nQualification);
				_context.SaveChanges();

				ViewBag.Message = "Your qualification was successfully added. Please check the qualifications table to see if it has been created";

			}
			return View();
		}


		public IActionResult UpdateQualification(int id)
		{
			DashboardOptions();

			_httpContextAccessor.HttpContext.Session.SetString("QualificationId", Convert.ToString(id));
			using (GlobalDBContext context = new GlobalDBContext())
			{
				Qualification qualification = context.Qualifications.Find(id);
				return View(qualification);
			}
		}

		[HttpPost]
		public IActionResult UpdateQualification(Qualification UpdatedQualification)
		{
			DashboardOptions();

			using (GlobalDBContext context = new GlobalDBContext())
			{

				QualificationId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("QualificationId"));

				Qualification _qualification = context.Qualifications.FirstOrDefault(k => k.QualificationId == QualificationId);
				Qualification qualification = new Qualification();
				qualification = qualification.UpdateQualification(_qualification, UpdatedQualification);
				context.Qualifications.Update(qualification);
				context.SaveChanges();

				ViewBag.SuccessMessage = "Your qualification at " + qualification.QualificationSchool + " has been updated successfully";

				return View(qualification);
			}
		}


		public IActionResult DeleteQualification(int id)
		{
			DashboardOptions();

			_httpContextAccessor.HttpContext.Session.SetString("QualificationId", Convert.ToString(id));
			using (GlobalDBContext context = new GlobalDBContext())
			{
				
				Qualification qualification = context.Qualifications.Find(id);
				context.Entry(qualification).State = EntityState.Deleted;
				context.SaveChanges();
			}	
			return Redirect("/DashboardStudent/Qualifications");
		}

			public IActionResult Documents()
		{
			DashboardOptions();

			// Below is code to access the database and retrieve the users current documents
			// GlobalDBContext context = new GlobalDBContext();

			//var documents = context.Documents.Where(u => u.User == _user).ToList();

			//UserDocument user = new UserDocument(documents[0]);

			return View();

		}

		[HttpPost]
		public IActionResult Documents(UserDocument document, IFormFile UserCLFile, IFormFile UserCVFile)
		{
			DashboardOptions();
			var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

			string uniqueCLFileName = null;
			string uniqueCVFileName = null;

			using (GlobalDBContext _context = new GlobalDBContext())
			{
				// This checks if the user has not selected a file to upload
				if (UserCLFile == null && UserCVFile == null)
				{
					// User will receive error message to upload a file.
					ViewBag.NoFileUploadedErrorMessage = "No files selected. Please select a file to upload";
				}

				// This checks if the user has selected to upload a Cover Letter AND CV
				if (UserCLFile != null && UserCLFile.Length > 0 && UserCVFile != null && UserCVFile.Length > 0)
				{
					Console.WriteLine(UserCLFile.FileName);
					Console.WriteLine(UserCVFile.FileName);

					// Check the file type - only accept PDF or Word. Receive error message if file is incorrect
					string CLfileExtension = System.IO.Path.GetExtension(UserCLFile.FileName);
					string CVfileExtension = System.IO.Path.GetExtension(UserCVFile.FileName);// Get the file extension and store as variable

					// Check if the Cover Letter file extension is PDF or Word
					if (CLfileExtension.ToLower() != ".doc" && CLfileExtension.ToLower() != ".docx" && CLfileExtension.ToLower() != ".pdf")
					{
						// User will receive error message if the file is not a Word or PDF document
						ViewBag.CLFileExtErrorMessage = "Files with .doc, .docx and .pdf allowed only.";
					}
					else
					{
						UserCL newUserCL = new UserCL();
						User user = _context.Users.Find(User_id);

						uniqueCLFileName = Guid.NewGuid().ToString() + "_" + UserCLFile.FileName;
						string folderPath = _env.WebRootPath + @"\uploads\UserCL\";
						string CLFilePath = folderPath + uniqueCLFileName;

						FileStream stream = new FileStream(CLFilePath, FileMode.Create);
						UserCLFile.CopyTo(stream);
						stream.Dispose();															

						// This creates the new cover letter and updates the database
						newUserCL.AddNewCL(newUserCL, user);
						_context.UserCL.Add(newUserCL);
						_context.SaveChanges();
					}

					// Check if the CV file extensions is PDF or Word
					if (CVfileExtension.ToLower() != ".doc" && CVfileExtension.ToLower() != ".docx" && CVfileExtension.ToLower() != ".pdf")
					{
						// User will receive error message if the file is not a Word or PDF document
						ViewBag.CVFileExtErrorMessage = "Files with .doc, .docx and .pdf allowed only.";
					}
					else
					{
						UserCV newUserCV = new UserCV();
						User user = _context.Users.Find(User_id);

						uniqueCVFileName = Guid.NewGuid().ToString() + "_" + UserCVFile.FileName;
						string folderPath = _env.WebRootPath + @"\uploads\UserCV\";
						string CVFilePath = folderPath + uniqueCVFileName;
						FileStream stream = new FileStream(CVFilePath, FileMode.Create);
						UserCVFile.CopyTo(stream);
						stream.Dispose();
						newUserCV.UserCVFullPath = CVFilePath;

						// This creates the new CV and updates the database
						newUserCV.AddNewCV(newUserCV, user);
						_context.UserCV.Add(newUserCV);
						_context.SaveChanges();
					}

					ViewBag.SuccessMessage = "Your Cover Letter and CV was successfully uploaded.";

				}

				// Check if the user only wants to upload their Cover Letter
				if (UserCLFile != null && UserCLFile.Length > 0 && UserCVFile == null)
				{
					Console.WriteLine(UserCLFile.FileName);
					// 2. Check the file type - only accept PDF or Word. Receive error message if file is incorrect
					string CLfileExtension = System.IO.Path.GetExtension(UserCLFile.FileName); // Get the file extension and store as variable

					// Check if the file extensions is PDF or Word
					if (CLfileExtension.ToLower() != ".doc" && CLfileExtension.ToLower() != ".docx" && CLfileExtension.ToLower() != ".pdf")
					{
						ViewBag.CLFileExtErrorMessage = "Files with .doc, .docx and .pdf allowed only.";
					}
					else
					{
						UserCL newUserCL = new UserCL();
						User user = _context.Users.Find(User_id);

						uniqueCLFileName = Guid.NewGuid().ToString() + "_" + UserCLFile.FileName;
						string folderPath = _env.WebRootPath + @"\uploads\UserCL\";
						string CLFilePath = folderPath + uniqueCLFileName;

						Console.WriteLine(newUserCL);
						FileStream stream = new FileStream(CLFilePath, FileMode.Create);
						UserCLFile.CopyTo(stream);
						stream.Dispose();

						newUserCL.UserClfullPath = CLFilePath;

						// This creates the new cover letter and updates the database
						newUserCL.AddNewCL(newUserCL, user);
						_context.UserCL.Add(newUserCL);
						_context.SaveChanges();

						ViewBag.SuccessMessage = "Your Cover Letter was successfully uploaded.";
					}
				}

				// Check if the user only wants to upload their CV
				if (UserCLFile == null && UserCVFile != null && UserCVFile.Length > 0)
				{
					//1.Check if there is a file

					Console.WriteLine(UserCVFile.FileName);
					// 2. Check the file type - only accept PDF or Word. Receive error message if file is incorrect
					string CVfileExtension = System.IO.Path.GetExtension(UserCVFile.FileName); // Get the file extentsion and store as ariable

					// Check if the file extensions is PDF or Word
					if (CVfileExtension.ToLower() != ".doc" && CVfileExtension.ToLower() != ".docx" && CVfileExtension.ToLower() != ".pdf")
					{
						ViewBag.CVFileExtErrorMessage = "Files with .doc, .docx and .pdf allowed only.";
					}
					else
					{
						UserCV newUserCV = new UserCV();
						User user = _context.Users.Find(User_id);

						uniqueCVFileName = Guid.NewGuid().ToString() + "_" + UserCVFile.FileName;
						string folderPath = _env.WebRootPath + @"\uploads\UserCV\";
						string CVFilePath = folderPath + uniqueCVFileName;
						FileStream stream = new FileStream(CVFilePath, FileMode.Create);
						UserCVFile.CopyTo(stream);
						stream.Dispose();
						newUserCV.UserCVFullPath = CVFilePath;

						// This creates the new CV and updates the database
						newUserCV.AddNewCV(newUserCV, user);
						_context.UserCV.Add(newUserCV);
						_context.SaveChanges();


						ViewBag.SuccessMessage = "Your CV was successfully uploaded.";
					}
				}

				return View(document);
			}
		}
				
	
		public IActionResult Experience()
		{
			DashboardOptions();

			using (GlobalDBContext _context = new GlobalDBContext())
			{
				// Gets all experiences created by the user
				var experience = _context.Experiences.ToList();
				return View(experience);
			}
		}

		public IActionResult AddExperience()
		{
			DashboardOptions();

			return View();
		}


		[HttpPost]
		public IActionResult AddExperience(Experience NewExperience)
		{
			DashboardOptions();

			var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

			using (GlobalDBContext _context = new GlobalDBContext())
			{

				Experience nExperience = new Experience();
				User user = _context.Users.Find(User_id);

				// This creates the new experience and updates the database
				nExperience.AddNewExperience(NewExperience, user);
				_context.Experiences.Add(nExperience);
				_context.SaveChanges();

				ViewBag.Message = "Your experience was successfully added. Please check the experiences table to see if it has been created";

			}
			return Redirect("/DashboardStudent/Experience");
		}

		public IActionResult UpdateExperience(int id)
		{
			DashboardOptions();

			_httpContextAccessor.HttpContext.Session.SetString("ExperienceId", Convert.ToString(id));
			using (GlobalDBContext context = new GlobalDBContext())
			{
				Experience experience = context.Experiences.Find(id);
				return View(experience);
			}
		}

		[HttpPost]
		public IActionResult UpdateExperience(Experience UpdatedExperience)
		{
			DashboardOptions();

			using (GlobalDBContext context = new GlobalDBContext())
			{

				ExperienceId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("ExperienceId"));

				Experience _experience = context.Experiences.FirstOrDefault(k => k.ExperienceId == ExperienceId);
				Experience experience = new Experience();
				experience = experience.UpdateExperience(_experience, UpdatedExperience);
				context.Experiences.Update(experience);
				context.SaveChanges();

				ViewBag.SuccessMessage = "Your experience at " + experience.ExperienceCompany + " has been updated successfully";

				return View(experience);
			}
		}

		public IActionResult DeleteExperience(int id)
		{
			DashboardOptions();

			_httpContextAccessor.HttpContext.Session.SetString("ExperienceId", Convert.ToString(id));
			using (GlobalDBContext context = new GlobalDBContext())
			{

				Experience experience = context.Experiences.Find(id);
				context.Entry(experience).State = EntityState.Deleted;
				context.SaveChanges();
			}
			return Redirect("/DashboardStudent/Experience");
		}

		public IActionResult Skills()
		{
			DashboardOptions();

			using (GlobalDBContext _context = new GlobalDBContext())
			{
				// Gets all skills created by the user
				var skills = _context.Skills.ToList();
				return View(skills);
			}
		}
		public IActionResult AddSkill()
		{
			DashboardOptions();

			return View();
		}


		[HttpPost]
		public IActionResult AddSkill(Skill NewSkill)
		{
			DashboardOptions();

			var User_id = _customAuthManager.Tokens.FirstOrDefault().Value.Item3;

			using (GlobalDBContext _context = new GlobalDBContext())

			{
				User user = _context.Users.Find(User_id);
				Console.WriteLine(user);

				StudentInternProfile intern = context.StudentInternProfiles.FirstOrDefault(k => k.User.UserId == User_id);
				Skill nSkill = new Skill();
				Console.WriteLine(nSkill.SkillID);
				Console.WriteLine(intern.StudentInternProfileId);
				
				

				// This creates the new skill and updates the database
				nSkill.AddNewSkill(NewSkill, intern);
				_context.Skills.Add(nSkill);
				_context.SaveChanges(); // Getting error: SqlException: Cannot insert explicit value for identity column in table 'StudentInternProfiles' when IDENTITY_INSERT is set to OFF.

				ViewBag.Message = "Your skill was successfully added. Please check the skill table to see if it has been created";

			}
			return View();
		}


		// MyApplications page works on 17 Oct 2020.
		public IActionResult MyApplications()
		{

			DashboardOptions();

			return View();
		}



		public async Task<IActionResult> CurrentVacancies([FromQuery] string searchTerm, int pageNumber = 0, int pageSize = 0)
		{
			DashboardOptions();
			/// What is happening How you get Internship -> Database to API. Api to you as a JSON response. when you can do something with that response
			/// In my custom response you get number of items, also you get what page you are on. And how many page to expect.
			/// Using QueryString, you can tell api what page you want, and how many internship info you want.

			IEnumerable<Internship> model;
			HttpResponseMessage resp;
			string InternshipUrl = host + Internship_url;
			string tempInternshipUrl;

			ViewData["SearchTerm"] = searchTerm;
			try
			{
				if (!String.IsNullOrEmpty(searchTerm))
				{
					InternshipUrl = InternshipUrl + "?search=" + searchTerm;
					//InternshipUrl = InternshipUrl;
					if (pageNumber != 0 && pageSize != 0)
					{
						InternshipUrl += "&pageNumber=" + pageNumber.ToString() + "&pageSize=" + pageSize.ToString();
					}
				}
				else
				{
					if (pageNumber != 0 && pageSize != 0)
					{
						InternshipUrl += "?pageNumber=" + pageNumber.ToString() + "&pageSize=" + pageSize.ToString();
					}
				}

				resp = await _client.GetAsync(InternshipUrl);
				resp.EnsureSuccessStatusCode();
				string responseBody = await resp.Content.ReadAsStringAsync();
				if (responseBody == "400")
				{
					ModelState.AddModelError("KeywordNotFound", "No Internships match the entered keyword.");
					tempInternshipUrl = InternshipUrl.Replace("?search=" + searchTerm, null);
					InternshipUrl = tempInternshipUrl;
					resp = await _client.GetAsync(InternshipUrl);
					resp.EnsureSuccessStatusCode();
					responseBody = await resp.Content.ReadAsStringAsync();
				}
				var data = JsonConvert.DeserializeObject<dynamic>("[" + responseBody + "]");
				ViewBag.pageSize = data[0]["pageSize"];
				ViewBag.totalPages = data[0]["totalPages"];
				ViewBag.currentPage = data[0]["pageNumber"];
				model = data[0]["data"].ToObject<IEnumerable<Internship>>();
				var intern = data[0]["data"][0];
				return View("CurrentVacancies", model);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
