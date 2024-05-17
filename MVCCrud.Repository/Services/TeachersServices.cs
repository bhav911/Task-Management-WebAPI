using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Interface;
using MVCCrud.Sessions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Services
{
    public class TeachersServices : ITeacherInterface
    {
        private readonly TaskManagement_490Entities _context = new TaskManagement_490Entities();

        public void RegisterTeacher(Teachers newTeacher)
        {
            try
            {
                _context.Teachers.Add(newTeacher);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Boolean DoesTeacherExist(string username)
        {
            try
            {
                bool result = _context.Teachers.FirstOrDefault(n => n.Username == username) != null;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Teachers AuthenticTeacher(LoginModel credentials)
        {
            try
            {
                Teachers requestingTeacher = _context.Teachers.FirstOrDefault(n => n.Username == credentials.login_username);
                return (requestingTeacher != null && requestingTeacher.Password == credentials.login_password) ? requestingTeacher : null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AssignTaskToStudent(int[] studentIds, int taskID)
        {
            try
            {
                if (studentIds == null)
                {
                    _context.SaveChanges();
                    return;
                }
                foreach (int ID in studentIds)
                {
                    Assignment temp = new Assignment();
                    temp.StudentID = ID;
                    temp.TaskID = taskID;
                    temp.Status = false;
                    _context.Assignment.Add(temp);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Assignment> GetTaskSpecificStudentList(int taskID)
        {
            try
            {
                List<Assignment> studentList = _context.Assignment.Where(t => t.TaskID == taskID).ToList();
                return studentList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TeacherDashboardModel GetDashboardInfo(int teacherID)
        {
            try
            {
                TeacherDashboardModel result = new TeacherDashboardModel();
                List<Assignment> list = _context.Assignment.ToList();
                int assignedTasks = list.Where(t => t.Tasks.CreatorID == teacherID).GroupBy(t => t.TaskID).Count();
                int pendingTasks = (from t in _context.Tasks
                                    join a in _context.Assignment on t.TaskID equals a.TaskID into temp
                                    from at in temp.DefaultIfEmpty()
                                    where t.CreatorID == teacherID && at == null
                                    select t).Count();
                int numberOfStudentPending = list.Where(t => t.Tasks.CreatorID == teacherID && t.Status == false).Count();
                int numberOfStudentSubmitted = list.Where(t => t.Tasks.CreatorID == teacherID && t.Status == true).Count();
                result.TasksAssigned = assignedTasks;
                result.TasksPending = pendingTasks;
                result.TotalTasks = assignedTasks + pendingTasks;
                result.NumberOfStudentSubmitted = numberOfStudentSubmitted;
                result.NumberOfStudentPending = numberOfStudentPending;
                result.NumberOfStudentAssigned = numberOfStudentPending + numberOfStudentSubmitted;

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
