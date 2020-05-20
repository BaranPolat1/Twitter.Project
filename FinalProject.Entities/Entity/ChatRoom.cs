using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class ChatRoom:KernelEntity
    {
        public virtual ICollection<ChatRoomUsers> ChatRoomUsers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
