using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
    public interface IRetweetService
    {
        JsonRetweetVM Retweet(Guid Id, string userName);
        IList<RetweetDTO> GetByFollowed(string userName);
        IList<RetweetDTO> GetByUser(string userId);
        IList<RetweetDTO> GetByTweet(Guid Id);
    }
}
