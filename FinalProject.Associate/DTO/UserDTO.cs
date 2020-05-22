using FinalProject.Associate.Helper;
using FinalProject.Entities.Entity;
using FinalProject.Kernel.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class UserDTO:BaseDTO
    {
        public bool OnlineMi { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public virtual ICollection<Follow> Follower { get; set; }
        public virtual ICollection<Follow> Followed { get; set; }
     





    }
}
