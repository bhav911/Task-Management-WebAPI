using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Models.CustomModel
{
    public class NewRegistrationModel
    {
        [RegularExpression("^(Student|Teacher)$", ErrorMessage = "Please Select a role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "only alphanumeric and '_'")]
        [MaxLength(length: 16, ErrorMessage = "Length should be less than 16")]
        [MinLength(length: 8, ErrorMessage = "Length Should be greater than 8")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email format (eg : abc@xyz.com)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Password Length should be 8-20 digits consisting of atleast 1 uppercase, lowercase and special symbol")]
        public string og_pass { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        [Compare("og_pass", ErrorMessage = "Password does not match")]
        public string cnf_pass { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Length Must be 10 digits")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Can't leave field Empty")]
        public int CityID { get; set; }
    }

}
