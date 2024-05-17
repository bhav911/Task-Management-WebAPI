using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Models.CustomModel
{
    public class AssignmentModel
    {
        public int AssignmentID { get; set; }
        public Nullable<int> TaskID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> submissionDate { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public string StudentUsername { get; set; }
        public string TeacherUsername { get; set; }
    }
}
