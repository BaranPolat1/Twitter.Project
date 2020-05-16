using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class Image:KernelEntity
    {
        public string Path { get; set; }

        public Guid TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}
