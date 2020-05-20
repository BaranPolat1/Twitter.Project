using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Concrete;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.DataAccess.Repository.Concrete
{
   public class ChatRoomUsersRepositoryEF: EntityRepositoryEF<ChatRoomUsers>,IChatRoomUsersRepository
    {
        public ChatRoomUsersRepositoryEF(ProjectContext db) : base(db)
        {

        }

        public ProjectContext ProjectContext
        {
            get { return db as ProjectContext; }
        }

        public bool Any(string userId, string user2Id)
        {
            var ChatRoom = ProjectContext.ChatRooms.ToList();
            foreach (var item in ChatRoom)
            {
                var list = ProjectContext.ChatRoomUsers.Where(x => (x.UserId == userId && x.ChatRoomId == item.Id) && (x.UserId== user2Id && x.ChatRoomId == item.Id)).ToList();
                if (list != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
            
           
        }
    }
}
