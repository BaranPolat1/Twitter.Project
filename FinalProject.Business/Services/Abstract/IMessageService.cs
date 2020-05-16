using FinalProject.Associate.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
  public  interface IMessageService
    {
        public IList<MessageDTO> GetAll();
        IList<MessageDTO> GetOwnMessages(string userName);
        MessageDTO GetMessage(Guid Id);
        IList<MessageDTO> GetChatBox(string userName, string userName2);
        IList<MessageDTO> GetOwnLastMessage(string userName);
        public bool OkunduMu(string userName);
    }
}
