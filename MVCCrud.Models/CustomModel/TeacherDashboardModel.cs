using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Models.CustomModel
{
    public class TeacherDashboardModel
    {
        public int TotalTasks { get; set; }
        public int TasksAssigned { get; set; }
        public int TasksPending { get; set; }
        public int NumberOfStudentAssigned { get; set; }
        public int NumberOfStudentPending { get; set; }
        public int NumberOfStudentSubmitted { get; set; }
    }
}
