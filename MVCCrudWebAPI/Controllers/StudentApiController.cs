using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MVCCrudWebAPI.Controllers
{
    public class StudentApiController : ApiController
    {
        private readonly TaskServices _tasks = new TaskServices();
        private readonly StudentsServices _student = new StudentsServices();

        [HttpGet]
        [Route("api/StudentApi/ListOfTasks")]
        public List<AssignmentModel> ListOfTasks(int studentID)
        {
            try
            {
                List<Assignment> ListOfTask = _tasks.GetStudentTasks(studentID);
                List<AssignmentModel> result = ModelConverterHelper.ConvertListOfAssignmentToListOfAssignmentModel(ListOfTask);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/StudentApi/SubmitTask")]
        public bool SubmitTask(int taskID, int studentID)
        {
            try
            {
                _tasks.SubmitTask(taskID, studentID);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("api/StudentApi/StudentDashboard")]
        public StudentDashboardModel StudentDashboard(int studentID)
        {
            try
            {
                StudentDashboardModel taskInfo = _student.GetDashboardInfo(studentID);
                return taskInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}