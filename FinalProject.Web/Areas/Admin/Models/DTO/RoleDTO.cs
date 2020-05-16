using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Web.Areas.Admin.Models.DTO
{
    public class RoleDTO
    {
        public class RoleDetails
        {
            public IdentityRole Role { get; set; }
            public IEnumerable<AppUser> Member { get; set; }
            public IEnumerable<AppUser> NonMember { get; set; }
        }

        public class RoleEditModel
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }

            public string[] IdToAdd { get; set; }
            public string[] IdToDelete { get; set; }
        }
    }
}
