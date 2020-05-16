using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Context;
using FinalProject.Entities.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        public void Send(string user, string content, string recipientId)
        {
            var sender = _userService.GetByUserName(Context.User.Identity.Name);
            Message message = new Message
            {
                Content = content,
                RecipientId = recipientId,
                SenderId = sender.Id
            };
            _db.Message.Add(message);
            _db.SaveChange();
            Clients.All.SendAsync("ReceiveMessage", user, content);
        }

    }
}
