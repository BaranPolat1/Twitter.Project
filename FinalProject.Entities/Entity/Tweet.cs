using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class Tweet : KernelEntity
    {

        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Retweet> Retweets { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
