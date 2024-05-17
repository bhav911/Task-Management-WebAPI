using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Models.CustomModel
{
    public class LoginModel
    {
        [RegularExpression("^(Student|Teacher)$", ErrorMessage = "Please Select a role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public string login_username { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public string login_password { get; set; }
    }
}
