using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Associate.VM
{
    public class RegisterDTO
    {
       
        public string UserName { get; set; }
        
        public string Email { get; set; }
      
        public string Password { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
