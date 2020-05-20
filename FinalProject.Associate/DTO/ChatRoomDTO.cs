using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class ChatRoomDTO
    {
        public Guid Id { get; set; }
        public virtual ICollection<ChatRoomUsers> ChatRoomUsers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
