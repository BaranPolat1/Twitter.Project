using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Associate.VM
{
    public class RegisterDTO
    {
        [Required, MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string UserName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Wrong email type")]
        public string Email { get; set; }
        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum lenght is 4")]
        public string Password { get; set; }
        [Required,MinLength(2,ErrorMessage = "Minimum lenght is 2")]
        public string FirstName { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
