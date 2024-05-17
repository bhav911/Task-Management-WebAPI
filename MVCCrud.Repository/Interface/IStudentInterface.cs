using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Interface
{
    public interface IStudentInterface
    {
        void RegisterStudent(Students newStudent);
        Boolean DoesStudentExist(string username);
        Students AuthenticStudent(LoginModel credentials);
        List<Students> GetAllStudent();
        StudentDashboardModel GetDashboardInfo(int studentID);

    }
}
