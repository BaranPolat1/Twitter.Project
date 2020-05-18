using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;


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

        public void Send(string user, string content, string recipientId,string image)
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
            Clients.Users(recipientId, sender.Id).SendAsync("ReceiveMessage", user, content, image);
        }

    }
}
