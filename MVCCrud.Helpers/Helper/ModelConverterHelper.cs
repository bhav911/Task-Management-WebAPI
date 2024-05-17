using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Helpers.Helper
{
    public class ModelConverterHelper
    {
        public static Students convertStudentModalToStudent(NewRegistrationModel StudentModel)
        {
            Students newStudent = new Students()
            {
                Username = StudentModel.Username,
                Address = StudentModel.Address,
                ContactNumber = StudentModel.ContactNumber,
                Email = StudentModel.Email,
                Password = StudentModel.og_pass,
                CityID = StudentModel.CityID,
                StateID = StudentModel.StateID
            };

            return newStudent;
        }
                
        public static List<AssignStudentModel> convertStudentToAssignStudent(List<Students> StudentList)
        {
            List<AssignStudentModel> studentModelList = new List<AssignStudentModel>();

            foreach(Students student in StudentList)
            {
                AssignStudentModel temp = new AssignStudentModel()
                {
                    StudentID = student.StudentID,
                    StudentName = student.Username
                };
                studentModelList.Add(temp);
            }
            

            return studentModelList;
        }

        public static Teachers convertTeacherModalToTeacher(NewRegistrationModel TeacherModel)
        {
            Teachers newTeacher = new Teachers()
            {
                Username = TeacherModel.Username,
                Address = TeacherModel.Address,
                CityID = TeacherModel.CityID,
                ContactNumber = TeacherModel.ContactNumber,
                Email = TeacherModel.Email,
                Password = TeacherModel.og_pass,
                StateID = TeacherModel.StateID
            };

            return newTeacher;
        }

        public static List<StateModel> ConvertStateToStateModel(List<States> stateList)
        {
            List<StateModel> result = new List<StateModel>();

            foreach(States item in stateList)
            {
                StateModel temp = new StateModel();
                temp.StateID = item.StateID;
                temp.StateName = item.StateName;
                result.Add(temp);
            }

            return result;
        }

        public static List<CityModel> ConvertCityToCityModel(List<Cities> cityList)
        {
            List<CityModel> result = new List<CityModel>();

            foreach (Cities item in cityList)
            {
                CityModel temp = new CityModel();
                temp.CityID = item.CityID;
                temp.CityName = item.CityName;
                result.Add(temp);
            }

            return result;
        }

        public static Tasks ConvertTaskModelToTask(TaskModel taskModel)
        {
            Tasks convertedTask = new Tasks()
            {
                CreatorID = Sessions.UserSession.UserID,
                Description = taskModel.Description,
                TaskName = taskModel.TaskName,
                Deadline = taskModel.Deadline,
            };

            convertedTask.CreatorID = UserSession.UserID;

            return convertedTask;
        }

        public static TaskModel ConvertTaskToTaskModel(Tasks task)
        {
            TaskModel convertedTask = new TaskModel()
            {
                CreatorID = task.CreatorID,
                Description = task.Description,
                TaskName = task.TaskName,
                Deadline = task.Deadline,
                TaskID = task.TaskID,
            };

            return convertedTask;
        }
        public static List<TaskModel> ConvertTaskListToTaskModelList(List<Tasks> tasks)
        {
            List<TaskModel> convertedList = new List<TaskModel>();
            foreach(Tasks task in tasks)
            {
                TaskModel temp = new TaskModel()
                {
                    CreatorID = task.CreatorID,
                    Description = task.Description,
                    TaskName = task.TaskName,
                    Deadline = task.Deadline,
                    TaskID = task.TaskID                    
            };
                convertedList.Add(temp);
            }

            return convertedList;
        }

        public static List<SubmissionStatusModel> ConvertAssignmentListToSubmissionStatusList(List<Assignment> studentList)
        {
            List<SubmissionStatusModel> result = new List<SubmissionStatusModel>();

            foreach(Assignment item in studentList)
            {
                SubmissionStatusModel temp = new SubmissionStatusModel()
                {
                    StudentID = (int)item.StudentID,
                    username = item.Students.Username,
                    status = (bool)item.Status
                };
                result.Add(temp);
            }
            return result;
        }

        public static List<AssignmentModel> ConvertListOfAssignmentToListOfAssignmentModel(List<Assignment> listOfAssignments)
        {
            List<AssignmentModel> result = new List<AssignmentModel>();

            foreach (Assignment item in listOfAssignments)
            {
                AssignmentModel temp = new AssignmentModel()
                {
                    AssignmentID = item.AssignmentID,
                    Deadline = item.Tasks.Deadline,
                    Description = item.Tasks.Description,
                    Status = item.Status,
                    StudentID = item.Students.StudentID,
                    submissionDate = item.submissionDate,
                    TaskID = item.TaskID,
                    TaskName = item.Tasks.TaskName,
                    StudentUsername = item.Students.Username,
                    TeacherUsername = item.Tasks.Teachers.Username
                };
                result.Add(temp);
            }
            return result;
        }
    }
}
