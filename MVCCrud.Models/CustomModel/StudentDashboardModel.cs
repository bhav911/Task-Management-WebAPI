using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Models.CustomModel
{
    public class StudentDashboardModel
    {
        public int TotalTask { get; set; }
        public int TaskCompleted { get; set; }
        public int TaskPending { get; set; }
    }
}
