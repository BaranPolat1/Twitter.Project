using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Context;
using FinalProject.Entities.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{

    public class MessageService : IMessageService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
       
        public MessageService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
         
        }
        public IList<MessageDTO> GetAll()
        {
            var message = _uow.Message.GetAll().OrderByDescending(x => x.CreatedDate);
            IList<MessageDTO> model = _mapper.Map<IList<MessageDTO>>(message);
            return model;
        }

       //Parametreden gelen kullanıcı adına göre, kullanıcının benimle olan mesajlarını bu metod ile listeliyorum.
        public IList<MessageDTO> GetChatBox(string userName, string userName2)
        {
            var a = _uow.User.Find(x => x.UserName == userName);
            var b = _uow.User.Find(x => x.UserName == userName2);
            var message = _uow.Message.FindByList(x => (x.RecipientId == a.Id && x.SenderId == b.Id) || (x.RecipientId == b.Id && x.SenderId == a.Id));
            IList<MessageDTO> model = _mapper.Map<IList<MessageDTO>>(message);
            return model;
        }

        //Dinamik olarak ChatRoom yaratan metodum.
        public ChatRoomDTO GetChatRoom(string userName, string userName2)
        {
            ChatRoom chat = new ChatRoom();
            ChatRoomDTO model = new ChatRoomDTO();
            var user1 = _uow.User.Find(x => x.UserName == userName);
            var user2 = _uow.User.Find(x => x.UserName == userName2);
            var chatBoxUser = _uow.ChatRoomUsers.FindByList(x => x.UserId == user1.Id);
            List<ChatRoom> chatRooms = new List<ChatRoom>();
            List<ChatRoomUsers> chatroomUsers = new List<ChatRoomUsers>();
           
            if (chatBoxUser.Count != 0)
            {
                foreach (var item in chatBoxUser)
                {
                    chatRooms.AddRange(_uow.ChatRoom.FindByList(x => x.Id == item.ChatRoomId));
                }

                foreach (var item in chatRooms)
                {
                    chatroomUsers.AddRange(_uow.ChatRoomUsers.FindByList(x => x.ChatRoomId == item.Id && x.UserId == user2.Id));

                }
                if (chatroomUsers.Count != 0)
                {
                    foreach (var item in chatroomUsers)
                    {
                        //messages1.AddRange(_uow.Message.FindByList(x => x.ChatRoomId == item.ChatRoomId));
                        chat = _uow.ChatRoom.GetById(item.ChatRoomId);
                    }
                }
                else
                {
                 
                    _uow.ChatRoom.Add(chat);
                    ChatRoomUsers chatRoom = new ChatRoomUsers();
                    chatRoom.ChatRoomId = chat.Id;
                    chatRoom.UserId = user1.Id;
                    _uow.ChatRoomUsers.Add(chatRoom);
                    ChatRoomUsers chatRoom1 = new ChatRoomUsers();
                    chatRoom1.ChatRoomId = chat.Id;
                    chatRoom1.UserId = user2.Id;
                    _uow.ChatRoomUsers.Add(chatRoom1);
                    _uow.SaveChange();
                   
                }

            }
            else
            {
                _uow.ChatRoom.Add(chat);
                ChatRoomUsers chatRoom = new ChatRoomUsers();
                chatRoom.ChatRoomId = chat.Id;
                chatRoom.UserId = user1.Id;
                _uow.ChatRoomUsers.Add(chatRoom);
                ChatRoomUsers chatRoom1 = new ChatRoomUsers();
                chatRoom1.ChatRoomId = chat.Id;
                chatRoom1.UserId = user2.Id;
                _uow.ChatRoomUsers.Add(chatRoom1);
                _uow.SaveChange();
               
            }
            model = _mapper.Map<ChatRoomDTO>(chat);
            return model;
        }

        public MessageDTO GetMessage(Guid Id)
        {
            var message = _uow.Message.Find(x => x.Id == Id);
            var model = _mapper.Map<MessageDTO>(message);
            return model;
        }

        public IList<MessageDTO> GetOwnLastMessage(string userName)
        {
            var user = _uow.User.Find(x => x.UserName == userName);
            var message = _uow.Message.FindByList(x => x.RecipientId == user.Id || x.SenderId == user.Id);
            List<AppUser> appusers = new List<AppUser>();
            List<Message> messages = new List<Message>();
            foreach (var item in message)
            {
                appusers.AddRange(_uow.User.FindByList(x => x.Id == item.RecipientId || x.Id == item.SenderId));
            }

            //mesajlaştığım kullanıcıları isimlerine göre gruplandırdım ve tekrar eden kullanıcı var ise sadece 1 tanesini aldım.
            var users = appusers.GroupBy(x => x.UserName).Select(p => p.Last()).ToList();

            //burada ise mesajlaşlaştığım kullanıcıların bilgilerin dolaştım. Eğer bana mesaj atan bir kullanıcı var ise ya da benim mesaj attığım bir kullanıcı var ise bu mesajın içeriğini (atılan son mesaj) mesaj kutusunda gösterdim. Bu sayede, bir kullanıcyla birden fazla mesajlaşmam olduysa mesaj kutusunda tek tek hepsini sıralamaktansa, tek satırda mesajın içeriğini gösterdim. Eğer tüm mesajları görmek istersem kullanıcının ismine tıklayıp tüm mesaj detaylarını gösteren syafaya gidebilecğeim.
            foreach (var item in users)
            {
                messages.AddRange(_uow.Message.FindByList(x => (x.RecipientId == item.Id && x.SenderId == user.Id) || (x.RecipientId == user.Id && x.SenderId == item.Id)).OrderBy(x => x.CreatedDate).TakeLast(1));
            }
            var list = messages.OrderByDescending(x => x.CreatedDate).ToList();
            var model = _mapper.Map<IList<MessageDTO>>(list);
            return model;


        }
        public IList<MessageDTO> GetOwnMessages(string userName)
        {
            var a = _uow.User.Find(x => x.UserName == userName);
            var messages = _uow.Message.FindByList(x => x.RecipientId == a.Id || x.SenderId == a.Id);
            IList<MessageDTO> model = _mapper.Map<IList<MessageDTO>>(messages);
            return model;
        }
        public bool OkunduMu(string userName)
        {
            var messages = GetOwnLastMessage(userName);
            if (messages.Any(x => x.OkunduMu == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
