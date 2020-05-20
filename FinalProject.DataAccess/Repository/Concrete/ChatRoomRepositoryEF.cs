using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Concrete;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.DataAccess.Repository.Concrete
{
    public class ChatRoomRepositoryEF:EntityRepositoryEF<ChatRoom>,IChatRoomRepository
    {
        public ChatRoomRepositoryEF(ProjectContext db):base(db)
        {

        }
        
        public ProjectContext ProjectContext
        {
            get { return db as ProjectContext; }
        }
    }
}
