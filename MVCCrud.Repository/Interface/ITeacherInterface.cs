using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Interface
{
    public interface ITeacherInterface
    {
        void RegisterTeacher(Teachers newTeacher);
        Boolean DoesTeacherExist(string username);
        Teachers AuthenticTeacher(LoginModel credentials);
        void AssignTaskToStudent(int[] studentIds, int taskID);
        List<Assignment> GetTaskSpecificStudentList(int taskID);
        TeacherDashboardModel GetDashboardInfo(int teacherID);

    }
}
