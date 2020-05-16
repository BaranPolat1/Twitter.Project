using FinalProject.Associate.Helper;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class TweetDTO:BaseDTO
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Retweet> Retweets { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
