using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MVCCrudWebAPI
{
    public class TeacherApiController : ApiController
    {
        private readonly TaskManagement_490Entities _context = new TaskManagement_490Entities();
        private readonly StudentsServices _student = new StudentsServices();
        private readonly TaskServices _tasks = new TaskServices();
        private readonly TeachersServices _teacher = new TeachersServices();

        [Route("api/TeacherApi/GetAllStudents")]
        public List<AssignStudentModel> GetAllStudents()
        {
            try
            {
                List<Students> tempResult = _student.GetAllStudent();
                List<AssignStudentModel> convertedStudentList = ModelConverterHelper.convertStudentToAssignStudent(tempResult);
                return convertedStudentList;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/TeacherApi/GetAssignedStudents")]
        public List<SubmissionStatusModel> GetAssignedStudents(int taskID)
        {
            try
            {
                List<Assignment> studentList = _teacher.GetTaskSpecificStudentList(taskID);
                List<SubmissionStatusModel> res = ModelConverterHelper.ConvertAssignmentListToSubmissionStatusList(studentList);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/GetSubmissionStatus")]
        public List<SubmissionStatusModel> GetSubmissionStatus(int taskID)
        {
            try
            {
                List<Assignment> studentList = _teacher.GetTaskSpecificStudentList(taskID);
                List<SubmissionStatusModel> result = ModelConverterHelper.ConvertAssignmentListToSubmissionStatusList(studentList);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/CreateTask")]
        public bool CreateTask(Tasks newTask)
        {
            try
            {
                _tasks.AddTask(newTask);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Route("api/TeacherApi/AssignTask")]
        public bool AssignTask(int[] studentList, int taskID)
        {
            try
            {
                _teacher.AssignTaskToStudent(studentList, taskID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Route("api/TeacherApi/TeacherDashboard")]
        [HttpGet]
        public TeacherDashboardModel TeacherDashboard(int teacherID)
        {
            try
            {
                TeacherDashboardModel taskInfo = _teacher.GetDashboardInfo(teacherID);  
                return taskInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/GetPendingStudentList")]
        public List<AssignmentModel> GetPendingStudentList(int pageNumber, int maxRecord, int teacherID)
        {
            try
            {
                List<Assignment> result = _tasks.GetPendingStudentList(pageNumber, maxRecord, teacherID);
                List<AssignmentModel> res = ModelConverterHelper.ConvertListOfAssignmentToListOfAssignmentModel(result);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/GetPendingStudentListCount")]
        public int GetPendingStudentListCount(int teacherID)
        {
            try
            {
                int result = _tasks.GetPendingStudentListCount(teacherID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/TeacherApi/GetSubmittedStudentList")]
        public List<AssignmentModel> GetSubmittedStudentList(int pageNumber, int maxRecord, int teacherID)
        {
            try
            {
                List<Assignment> result = _tasks.GetSubmittedStudentList(pageNumber, maxRecord, teacherID);
                List<AssignmentModel> res = ModelConverterHelper.ConvertListOfAssignmentToListOfAssignmentModel(result);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/GetSubmittedStudentListCount")]
        public int GetSubmittedStudentListCount(int teacherID)
        {
            try
            {
                int result = _tasks.GetSubmittedStudentListCount(teacherID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/TeacherApi/GetAllStudentList")]
        public List<AssignmentModel> GetAllStudentList(int pageNumber, int maxRecord, int teacherID)
        {
            try
            {
                List<Assignment> result = _tasks.GetAllStudentList(pageNumber, maxRecord, teacherID);
                List<AssignmentModel> res = ModelConverterHelper.ConvertListOfAssignmentToListOfAssignmentModel(result);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/GetAllStudentListCount")]
        public int GetAllStudentListCount(int teacherID)
        {
            try
            {
                int result = _tasks.GetAllStudentListCount(teacherID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/TeacherApi/GetTasks")]
        public List<TaskModel> GetTasks(int pageNumber, int maxRecord, int teacherID)
        {
            try
            {
                List<Tasks> result = _tasks.GetTasks(pageNumber, maxRecord, teacherID);
                List<TaskModel> res = ModelConverterHelper.ConvertTaskListToTaskModelList(result);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("api/TeacherApi/GetTasksCount")]
        public int GetTasksCount(int teacherID)
        {
            try
            {
                int result = _tasks.GetTasksCount(teacherID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}