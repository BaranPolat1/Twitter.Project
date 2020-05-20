using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{

    public class ChatRoomService : IChatRoomService
    {
        private IUnitOfWork _uow;
        public ChatRoomService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //public  IList<ChatRoom> Add(string userName, string userName2)
        //{
        //    var user1 = _uow.User.Find(x => x.UserName == userName);
        //    var user2 = _uow.User.Find(x => x.UserName == userName2);
        //    var chatRoomUsers = _uow.ChatRoomUsers.FindByList(x => x.AppUserId == user1.Id && x.AppUserId == user2.Id);
        //    if (chatRoomUsers != null)
        //    {
        //        foreach (var item in chatRoomUsers)
        //        {
        //            ChatRoom chat1 = _uow.ChatRoom.Find(x => x.Id == item.ChatRoomId);

        //            return chat1;
        //        }
        //    }
        //    else
        //    {
        //        ChatRoom chat = new ChatRoom();
        //        _uow.ChatRoom.Add(chat);
        //        ChatRoomUsers chatRoom = new ChatRoomUsers();
        //        chatRoom.ChatRoomId = chat.Id;
        //        chatRoom.AppUserId = user1.Id;
        //        _uow.ChatRoomUsers.Add(chatRoom);
        //        ChatRoomUsers chatRoom1 = new ChatRoomUsers();
        //        chatRoom1.ChatRoomId = chat.Id;
        //        chatRoom1.AppUserId = user2.Id;
        //        _uow.ChatRoomUsers.Add(chatRoom1);
        //        _uow.SaveChange();
        //        return chat;
        //    }
        //}

        ChatRoom IChatRoomService.Add(string userName, string userName2)
        {
            throw new NotImplementedException();
        }
    }
}
