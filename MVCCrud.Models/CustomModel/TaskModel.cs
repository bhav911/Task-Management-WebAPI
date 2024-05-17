using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Models.CustomModel
{
    public class TaskModel
    {
        public int TaskID { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public string Description { get; set; }

        [DateNotOfPast]
        public Nullable<System.DateTime> Deadline { get; set; }
        public Nullable<int> CreatorID { get; set; }
    }

    public class DateNotOfPast : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date value should not be a Past date";
        }

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            //alter this as needed. I am doing the date comparison if the value is not null

            if (dateValue.Date < DateTime.Now.Date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
