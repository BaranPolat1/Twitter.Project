
using FinalProject.Kernel.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class AppUser : IdentityUser
    {
       
       public bool OnlineMi { get; set; } 
        public DateTime? BirthDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
        public Status Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConnectionId { get; set; }
        private string _imagePath = "yumurta.jpg";
        public string ImagePath { get { return _imagePath; } set { _imagePath = value; } }

        private DateTime? _createDate = DateTime.Now;
        public DateTime? CreatedDate { get { return _createDate; } set { _createDate = value; } }
        public string Bio { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Retweet> Retweets { get; set; }
        public virtual ICollection<ChatRoomUsers> ChatRoomUsers { get; set; }

        [InverseProperty("FollowerUser")]
        public virtual ICollection<Follow> Follower { get; set; }
        [InverseProperty("FollowedUser")]
        public virtual ICollection<Follow> Followed { get; set; }


        [InverseProperty("SenderUser")]
        public virtual ICollection<Message> Senders { get; set; }
        [InverseProperty("RecipientUser")]
        public virtual ICollection<Message> Recipients
        {
            get; set;
        }
    }
}
