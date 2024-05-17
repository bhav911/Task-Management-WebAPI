using MVCCrud.Common;
using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Services;
using MVCCrud.Sessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    [CustomAuthorizeAttributeHelper]
    [CustomHandleErrorAttribute]
    public class HomeController : Controller
    {
        private readonly TaskServices _tasks = new TaskServices();
        public ActionResult EntryAction(string name)
        {
            try
            {
                TempData["Role"] = UserSession.UserRole;
                if (!TempData.ContainsKey("Name"))
                {
                    TempData["Name"] = name;
                }
                if (UserSession.UserRole.Equals("Teacher"))
                    return RedirectToAction("TeacherDashboard", "Teacher");
                return RedirectToAction("StudentDashboard", "Student");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        public ActionResult PageNotFound()
        {
            return View("Error");
        }
        //private PaginationModel GetRequestedPage(int pageNumber, string serviceName)
        //{
        //    try
        //    {
        //        PaginationModel currentPage = new PaginationModel();

        //        int maxRecord = 5;

        //        List<Tasks> allTasks = new List<Tasks>();
        //        List<Assignment> studentList = new List<Assignment>();
        //        int countOfRecords = 1;
        //        switch (serviceName)
        //        {
        //            case "penStud":
        //                studentList = _tasks.GetPendingStudentList(pageNumber, maxRecord, UserSession.UserID);
        //                countOfRecords = _tasks.GetPendingStudentListCount(UserSession.UserID);
        //                break;
        //            case "subStud":
        //                studentList = _tasks.GetSubmittedStudentList(pageNumber, maxRecord, UserSession.UserID);
        //                countOfRecords = _tasks.GetSubmittedStudentListCount(UserSession.UserID);
        //                break;
        //            case "allStud":
        //                studentList = _tasks.GetAllStudentList(pageNumber, maxRecord, UserSession.UserID);
        //                countOfRecords = _tasks.GetAllStudentListCount(UserSession.UserID);
        //                break;
        //            case "taskList":
        //                allTasks = _tasks.GetTasks(pageNumber, maxRecord, UserSession.UserID);
        //                countOfRecords = _tasks.GetTasksCount(UserSession.UserID);
        //                break;
        //        }


        //        decimal intermediatePageCount = countOfRecords / (decimal)(maxRecord);

        //        currentPage.TotalPage = Convert.ToInt16(Math.Ceiling(intermediatePageCount));
        //        if (serviceName == "taskList")
        //        {
        //            currentPage.TaskList = allTasks;
        //        }
        //        else
        //        {
        //            currentPage.StudentList = studentList;
        //        }
        //        currentPage.CurrentPage = pageNumber;
        //        return currentPage;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}