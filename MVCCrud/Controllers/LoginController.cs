using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Services;
using MVCCrud.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult SignIn(LoginModel credentials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (credentials.Role.Equals("Student"))
                    {
                        Students authenticateStudent = _student.AuthenticStudent(credentials);
                        if (authenticateStudent != null)
                        {
                            UserSession.UserID = authenticateStudent.StudentID;
                            UserSession.UserName = authenticateStudent.Username;
                            UserSession.UserRole = credentials.Role;
                            TempData["smessage"] = "Log In Successfull";
                            return RedirectToAction("EntryAction", "Home", new { name = authenticateStudent.Username });
                        }
                    }
                    else if (credentials.Role.Equals("Teacher"))
                    {
                        Teachers authenticateTeacher = _teacher.AuthenticTeacher(credentials);
                        if (authenticateTeacher != null)
                        {
                            UserSession.UserID = authenticateTeacher.TeacherID;
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
        public ActionResult SignUp(NewRegistrationModel newPerson)
        {
            try
            {
                HttpContext.Session.Clear();
                if (ModelState.IsValid)
                {
                    if (newPerson.Role.Equals("Student"))
                    {
                        if (_student.DoesStudentExist(newPerson.Username))
                        {
                            TempData["wmessage"] = "Student is already registered";
                            return View(newPerson);
                        }
                        Students newStudent = ModelConverterHelper.convertStudentModalToStudent(newPerson);
                        _student.RegisterStudent(newStudent);
                        TempData["smessage"] = "Student Registered Successfully";
                    }
                    else if (newPerson.Role.Equals("Teacher"))
                    {
                        if (_teacher.DoesTeacherExist(newPerson.Username))
                        {
                            TempData["wmessage"] = "Admin is already registered";
                            return View(newPerson);
                        }
                        Teachers newTeacher = ModelConverterHelper.convertTeacherModalToTeacher(newPerson);
                        _teacher.RegisterTeacher(newTeacher);
                        TempData["smessage"] = "Teacher Registered Successfully";
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
        public JsonResult GetAllStates()
        {
            try
            {
                List<StateModel> states = _states.GetAllStates();
                return Json(states, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetAllCities(int stateID)
        {
            try
            {
                List<CityModel> cities = _states.GetAllCities(stateID);
                return Json(cities, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}