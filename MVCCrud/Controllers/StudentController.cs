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
    [CustomAuthorizeAttributeHelper]
    public class StudentController : Controller 
    {
        private readonly TaskServices _tasks = new TaskServices();
        private readonly StudentsServices _student = new StudentsServices();

        [HttpGet]
        public async Task<ActionResult> ListOfTasks()
        {
            try
            {
                if (UserSession.UserRole.Equals("Student"))
                {
                    string response = await WebApiHelper.HttpClientRequestResponseGet("api/StudentApi/ListOfTasks?studentID=" + UserSession.UserID);
                    List<AssignmentModel> ListOfTask = JsonConvert.DeserializeObject<List<AssignmentModel>>(response);
                    return View(ListOfTask);
                }
                return View("Unauthorize");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<JsonResult> SubmitTask(int taskID)
        {
            try
            {
                if (UserSession.UserRole.Equals("Student"))
                {
                    string response = await WebApiHelper.HttpClientRequestResponseGet($"api/StudentApi/SubmitTask?taskID={taskID}&studentID={UserSession.UserID}");
                    TempData["smessage"] = "Task Submitted Successfully";
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> StudentDashboard()
        {
            try
            {
                if (UserSession.UserRole.Equals("Student"))
                {
                    string response = await WebApiHelper.HttpClientRequestResponseGet("api/StudentApi/StudentDashboard?studentID=" + UserSession.UserID);
                    StudentDashboardModel taskInfo = JsonConvert.DeserializeObject<StudentDashboardModel>(response);
                    return View(taskInfo);
                }
                return View("Unauthorize");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
    }
}