using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class RetweetDTO:BaseDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        public Guid TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}
