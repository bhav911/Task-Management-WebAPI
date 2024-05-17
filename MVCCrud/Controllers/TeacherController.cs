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
    public class TeacherController : Controller
    {
        private readonly TaskServices _tasks = new TaskServices();
        private readonly TeachersServices _teacher = new TeachersServices();
        public ActionResult CreateTask()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    return View();
                }
                return View("Unauthorize");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskModel newTask)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    if (ModelState.IsValid)
                    {
                        Tasks convertedTask = ModelConverterHelper.ConvertTaskModelToTask(newTask);
                        string status = await WebApiHelper.HttpClientRequestResponsePost("api/TeacherApi/CreateTask", convertedTask, null, "createTask");
                        TempData["smessage"] = "Task Created Successfully";
                        return RedirectToAction("CreateTask");
                    }
                    return View();
                }
                return View("Unauthorize");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ListOfTaskForTeacher()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage(1, "taskList");
                    return View(result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        public async Task<ActionResult> ListOfTaskForTeacherManual(int pageNum)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage(pageNum, "taskList");
                    return View("ListOfTaskForTeacher", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AssignTask(int[] studentList, int taskID, int pageNum)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    string status = await WebApiHelper.HttpClientRequestResponsePost("api/TeacherApi/AssignTask?taskID=" + taskID, null, studentList, "AssignTask");
                    TempData["smessage"] = "Task Assigned Successfully";
                    return RedirectToAction("ListOfTaskForTeacherManual", new { pageNum });
                }
                return View("Unauthorize");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public async Task<JsonResult> GetAllStudents()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    string res = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetAllStudents");
                    List<AssignStudentModel> studentList = JsonConvert.DeserializeObject<List<AssignStudentModel>>(res);
                    return Json(studentList, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonResult> GetAssignedStudents(int taskID)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    string res = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetAssignedStudents?taskID=" + taskID);
                    List<SubmissionStatusModel> studentList = JsonConvert.DeserializeObject<List<SubmissionStatusModel>>(res);
                    int[] result = extractStudentID(studentList);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int[] extractStudentID(List<SubmissionStatusModel> studentList)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    int n = studentList.Count();
                    int[] result = new int[n];
                    for (int item = 0; item < n; item++)
                    {
                        result[item] = (int)studentList[item].StudentID;
                    }
                    return result;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> TeacherDashboard()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    string result = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/TeacherDashboard?teacherID=" + UserSession.UserID);
                    TeacherDashboardModel taskInfo = JsonConvert.DeserializeObject<TeacherDashboardModel>(result);
                    return View(taskInfo);
                }
                return View("Unauthorize");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public async Task<JsonResult> GetSubmissionStatus(int taskID)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                   string res = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetSubmissionStatus?taskID=" + taskID);
                    List<SubmissionStatusModel> result = JsonConvert.DeserializeObject<List<SubmissionStatusModel>>(res);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ActionResult> GetPendingStudentList()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage(1, "penStud");
                    ViewData["penList"] = true;
                    ViewData.Remove("fullList");
                    ViewData.Remove("subList");
                    return View("StatusSpecificList", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> GetSubmittedStudentList()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage(1, "subStud");
                    ViewData["subList"] = true;
                    ViewData.Remove("fullList");
                    ViewData.Remove("penList");
                    return View("StatusSpecificList", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> GetAllStudentList()
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage(1, "allStud");
                    ViewData["fullList"] = true;
                    ViewData.Remove("subList");
                    ViewData.Remove("penList");
                    return View("StatusSpecificList", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetPendingStudentList(int pageNum)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage (pageNum, "penStud");
                    ViewData.Remove("fullList");
                    return View("StatusSpecificList", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetSubmittedStudentList(int pageNum)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage (pageNum, "subStud");
                    ViewData.Remove("fullList");
                    return View("StatusSpecificList", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetAllStudentList(int pageNum)
        {
            try
            {
                if (UserSession.UserRole.Equals("Teacher"))
                {
                    PaginationModel result = await GetRequestedPage(pageNum, "allStud");
                    ViewData["fullList"] = true;
                    return View("StatusSpecificList", result);
                }
                return View("Unauthorize");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        public async Task<PaginationModel> GetRequestedPage(int pageNumber, string serviceName)
        {
            try
            {
                PaginationModel currentPage = new PaginationModel();

                int maxRecord = 5;

                List<TaskModel> allTasks = new List<TaskModel>();
                string result = "";
                List<AssignmentModel> studentList = new List<AssignmentModel>();
                int countOfRecords = 1;
                switch (serviceName)
                {
                    case "penStud":
                        result = await WebApiHelper.HttpClientRequestResponseGet($"api/TeacherApi/GetPendingStudentList?pageNumber={pageNumber}&maxRecord={maxRecord}&teacherID={UserSession.UserID}");
                        studentList = JsonConvert.DeserializeObject<List<AssignmentModel>>(result);
                        result = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetPendingStudentListCount?teacherID=" + UserSession.UserID);
                        countOfRecords = Convert.ToInt32(result);
                        break;
                    case "subStud":
                        result = await WebApiHelper.HttpClientRequestResponseGet($"api/TeacherApi/GetSubmittedStudentList?pageNumber={pageNumber}&maxRecord={maxRecord}&teacherID={UserSession.UserID}");
                        studentList = JsonConvert.DeserializeObject<List<AssignmentModel>>(result);
                        result = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetSubmittedStudentListCount?teacherID=" + UserSession.UserID);
                        countOfRecords = Convert.ToInt32(result);
                        break;
                    case "allStud":
                        result = await WebApiHelper.HttpClientRequestResponseGet($"api/TeacherApi/GetAllStudentList?pageNumber={pageNumber}&maxRecord={maxRecord}&teacherID={UserSession.UserID}");
                        studentList = JsonConvert.DeserializeObject<List<AssignmentModel>>(result);
                        result = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetAllStudentListCount?teacherID=" + UserSession.UserID);
                        countOfRecords = Convert.ToInt32(result);
                        break;
                    case "taskList":
                        result = await WebApiHelper.HttpClientRequestResponseGet($"api/TeacherApi/GetTasks?pageNumber={pageNumber}&maxRecord={maxRecord}&teacherID={UserSession.UserID}");
                        allTasks = JsonConvert.DeserializeObject<List<TaskModel>>(result);
                        result = await WebApiHelper.HttpClientRequestResponseGet("api/TeacherApi/GetTasksCount?teacherID=" + UserSession.UserID);
                        countOfRecords = Convert.ToInt32(result);
                        break;
                }


                decimal intermediatePageCount = countOfRecords / (decimal)(maxRecord);

                currentPage.TotalPage = Convert.ToInt16(Math.Ceiling(intermediatePageCount));
                if (serviceName == "taskList")
                {
                    currentPage.TaskList = allTasks;
                }
                else
                {
                    currentPage.StudentList = studentList;
                }
                currentPage.CurrentPage = pageNumber;
                return currentPage;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}