using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Entities.Entity
{
    public class Message : KernelEntity
    {
         public string Content { get; set; }
      
        [ForeignKey("SenderUser")]
        public string SenderId { get; set; }
        public virtual AppUser SenderUser { get; set; }

        [ForeignKey("RecipientUser")]
        public string RecipientId { get; set; }
        public virtual AppUser RecipientUser { get; set; }

        public Guid ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
