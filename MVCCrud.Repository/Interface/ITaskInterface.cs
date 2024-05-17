using MVCCrud.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Interface
{
    public interface ITaskInterface
    {
        void AddTask(Tasks newTask);
        List<Tasks> GetTasks(int pageNum, int maxPage, int TeacherID);
        int GetTasksCount(int TeacherID);
        List<Assignment> GetStudentTasks(int StudentID);
        void SubmitTask(int taskID, int studentID);
        List<Assignment> GetPendingStudentList(int pageNum, int maxPage, int TeacherID);
        List<Assignment> GetSubmittedStudentList(int pageNum, int maxPage, int TeacherID);
        List<Assignment> GetAllStudentList(int pageNum, int maxPage, int TeacherID);
        int GetPendingStudentListCount(int TeacherID);
        int GetSubmittedStudentListCount(int TeacherID);
        int GetAllStudentListCount(int TeacherID);


    }
}
