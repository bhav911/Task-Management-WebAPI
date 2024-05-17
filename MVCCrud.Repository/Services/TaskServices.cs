using MVCCrud.Models.Context;
using MVCCrud.Repository.Interface;
using MVCCrud.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Services
{
    public class TaskServices : ITaskInterface
    {
        private readonly TaskManagement_490Entities _context = new TaskManagement_490Entities();
        public void AddTask(Tasks newTask)
        {
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
        }

        public List<Tasks> GetTasks(int pageNum, int maxPage, int TeacherID)
        {
            List<Tasks> result = _context.Tasks.Where(t => t.CreatorID == TeacherID).OrderBy(t => t.TaskID).Skip((pageNum - 1) * maxPage).Take(maxPage).ToList();
            return result;
        }
        public int GetTasksCount(int TeacherID)
        {
            int count = _context.Tasks.Where(t => t.CreatorID == TeacherID).Count();
            return count;
        }

        public List<Assignment> GetStudentTasks(int StudentID)
        {
            List<Assignment> assignmentList = _context.Assignment.Where(s => s.StudentID == StudentID).ToList();
            return assignmentList;
        }

        public void SubmitTask(int taskID, int studentID)
        {
            Assignment record = _context.Assignment.Where(t => t.TaskID == taskID && t.StudentID == studentID).FirstOrDefault();
            record.Status = true;
            record.submissionDate = DateTime.Now;
            _context.SaveChanges();
        }

        public List<Assignment> GetPendingStudentList(int pageNum, int maxPage, int TeacherID)
        {
            List<Assignment> result = _context.Assignment.Where(t => t.Tasks.CreatorID == TeacherID && t.Status == false).OrderBy(t => t.TaskID).Skip((pageNum - 1) * maxPage).Take(maxPage).ToList();
            return result;
        }
        public List<Assignment> GetSubmittedStudentList(int pageNum, int maxPage, int TeacherID)
        {
            List<Assignment> result = _context.Assignment.Where(t => t.Tasks.CreatorID == TeacherID && t.Status == true).OrderBy(t => t.TaskID).Skip((pageNum - 1) * maxPage).Take(maxPage).ToList();
            return result;
        }
        public List<Assignment> GetAllStudentList(int pageNum, int maxPage, int TeacherID)
        {
            List<Assignment> result = _context.Assignment.Where(t => t.Tasks.CreatorID == TeacherID).OrderBy(t => t.TaskID).Skip((pageNum - 1) * maxPage).Take(maxPage).ToList();
            return result;
        }
        public int GetPendingStudentListCount(int TeacherID)
        {
            int count = _context.Assignment.Where(t => t.Tasks.CreatorID == TeacherID && t.Status == false).Count();
            return count;
        }
        public int GetSubmittedStudentListCount(int TeacherID)
        {
            int count = _context.Assignment.Where(t => t.Tasks.CreatorID == TeacherID && t.Status == true).Count();
            return count;
        }
        public int GetAllStudentListCount(int TeacherID)
        {
            int count = _context.Assignment.Where(t => t.Tasks.CreatorID == TeacherID).Count();
            return count;
        }
    }
}
