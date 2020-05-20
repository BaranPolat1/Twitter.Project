using FinalProject.Kernel.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Entities.Entity
{
   public class ChatRoomUsers
    {
     
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Guid ChatRoomId{ get; set; }
        public virtual ChatRoom ChatRoom { get; set; }

    }
}
