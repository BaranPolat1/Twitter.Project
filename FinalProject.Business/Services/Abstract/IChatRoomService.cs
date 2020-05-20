using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
  public interface IChatRoomService
    {
       ChatRoom Add(string userName, string userName2);
    }
}
