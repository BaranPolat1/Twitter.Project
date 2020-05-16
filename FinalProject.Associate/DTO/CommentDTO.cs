using FinalProject.Associate.Helper;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class CommentDTO:BaseDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
       
        public Guid TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }

    }
}
