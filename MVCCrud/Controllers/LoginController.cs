using MVCCrud.Common;
using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Services;
using MVCCrud.Sessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    public class LoginController : Controller
    {
        private readonly TeachersServices _teacher = new TeachersServices();
        private readonly StudentsServices _student = new StudentsServices();
        private readonly StateCityServices _states = new StateCityServices();


        public ActionResult SignIn()
        {
            try
            {
                HttpContext.Session.Clear();
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(LoginModel credentials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (credentials.Role.Equals("Student"))
                    {
                        string status = await WebApiHelper.HttpClientRequestResponsePost("api/LoginApi/SignIn", null, null, "SignIn", credentials, null);
                        SessionModel authenticateStudent = JsonConvert.DeserializeObject<SessionModel>(status);
                        if (authenticateStudent != null)
                        {
                            UserSession.UserID = authenticateStudent.UserID;
                            UserSession.UserName = authenticateStudent.Username;
                            UserSession.UserRole = credentials.Role;
                            TempData["smessage"] = "Log In Successfull";
                            return RedirectToAction("EntryAction", "Home", new { name = authenticateStudent.Username });
                        }
                    }
                    else if (credentials.Role.Equals("Teacher"))
                    {
                        string status = await WebApiHelper.HttpClientRequestResponsePost("api/LoginApi/SignIn", null, null, "SignIn", credentials, null);
                        SessionModel authenticateTeacher = JsonConvert.DeserializeObject<SessionModel>(status);
                        if (authenticateTeacher != null)
                        {
                            UserSession.UserID = authenticateTeacher.UserID;
                            UserSession.UserName = authenticateTeacher.Username;
                            UserSession.UserRole = credentials.Role;
                            TempData["smessage"] = "Log In Successfull";
                            return RedirectToAction("EntryAction", "Home", new { name = authenticateTeacher.Username });
                        }
                    }
                    TempData["emessage"] = "Incorrect Login Credentials";
                    return View(credentials);
                }
                return View(credentials);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult SignOut()
        {
            try
            {
                HttpContext.Session.Clear();
                TempData.Clear();
                return Redirect("SignIn");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult SignUp()
        {
            try
            {
                HttpContext.Session.Clear();
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(NewRegistrationModel newPerson)
        {
            try
            {
                HttpContext.Session.Clear();
                if (ModelState.IsValid)
                {
                    string response  = await WebApiHelper.HttpClientRequestResponsePost("api/LoginApi/SignUp", null, null, "SignUp", null, newPerson);
                    string status = JsonConvert.DeserializeObject<string>(response);
                    if(status.Equals("exist"))
                    {
                        TempData["wmessage"] = "User is already registered";
                        return View(newPerson);
                    }
                    else
                    {
                        TempData["smessage"] = "User Registered Successfully";
                    }
                    return RedirectToAction("SignIn");
                }
                TempData["emessage"] = "Form contains invalid fields";
                return View(newPerson);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }


        public ActionResult ForgotPassword()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        public async Task<JsonResult> GetAllStates()
        {
            try
            {
                string result = await WebApiHelper.HttpClientRequestResponseGet("api/LoginApi/GetAllStates");
                List<StateModel> states = JsonConvert.DeserializeObject<List<StateModel>>(result);
                return Json(states, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<JsonResult> GetAllCities(int stateID)
        {
            try
            {
                string result = await WebApiHelper.HttpClientRequestResponseGet("api/LoginApi/GetAllCities?stateID=" + stateID);
                //optional to DeserializeObject
                List<CityModel> cities = JsonConvert.DeserializeObject<List<CityModel>>(result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}