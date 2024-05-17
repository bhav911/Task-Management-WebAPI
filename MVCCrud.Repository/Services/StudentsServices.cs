using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Services
{
    public class StudentsServices : IStudentInterface
    {
        private readonly TaskManagement_490Entities _context = new TaskManagement_490Entities();

        public void RegisterStudent(Students newStudent)
        {
            try
            {
                _context.Students.Add(newStudent);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean DoesStudentExist(String username)
        {
            try
            {
                bool result = _context.Students.FirstOrDefault(n => n.Username == username) != null;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Students AuthenticStudent(LoginModel credentials)
        {
            try
            {
                Students requestingStudent = _context.Students.FirstOrDefault(n => n.Username == credentials.login_username);
                return (requestingStudent != null && requestingStudent.Password == credentials.login_password) ? requestingStudent : null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Students> GetAllStudent()
        {
            try
            {
                List<Students> result = _context.Students.ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public StudentDashboardModel GetDashboardInfo(int studentID)
        {
            try
            {
                StudentDashboardModel result = new StudentDashboardModel();
                List<Assignment> list = _context.Assignment.ToList();
                int completedTasks = list.Where(s => s.StudentID == studentID && s.Status == true).Count();
                int pendingTasks = list.Where(s => s.StudentID == studentID && s.Status == false).Count();
                result.TaskCompleted = completedTasks;
                result.TaskPending = pendingTasks;
                result.TotalTask = completedTasks + pendingTasks;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
