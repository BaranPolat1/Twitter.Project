using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Context;
using FinalProject.Entities.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Web.Hubs
{

    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private IAppUserService _userService;
        private IUnitOfWork _db;
        public ChatHub(IUnitOfWork db, IAppUserService userService)
        {
            _userService = userService;
            _db = db;
        }
        public void Send(string user, string content, string recipientId, string image,Guid group)
        {
            var user1 = _userService.GetByUserName(Context.User.Identity.Name);
            Groups.AddToGroupAsync(Context.ConnectionId, group.ToString());
            Message message = new Message
            {
                Content = content,
                RecipientId = recipientId,
                SenderId = user1.Id,
                ChatRoomId = group
                
            };
            _db.Message.Add(message);
            _db.SaveChange();
            Clients.Group(group.ToString()).SendAsync("ReceiveMessage", user, content, image);
        }
    }
}
