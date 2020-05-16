using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class Comment : KernelEntity
    {
        public string Content { get; set; }
    

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        public Guid TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}
