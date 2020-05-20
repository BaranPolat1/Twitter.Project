using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class MessageDTO:BaseDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool OkunduMu { get; set; }

        public string SenderId { get; set; }
        public virtual AppUser SenderUser { get; set; }

        public string RecipientId { get; set; }
        public virtual AppUser RecipientUser { get; set; }

        public Guid ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
