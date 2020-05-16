using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class Follow : KernelEntity
    {
        [ForeignKey("FollowerUser")]
        public string FollowerId { get; set; }
        public virtual AppUser FollowerUser { get; set; }

        [ForeignKey("FollowedUser")]
        public string FollowedId { get; set; }
        public virtual AppUser FollowedUser { get; set; }
    }
}
