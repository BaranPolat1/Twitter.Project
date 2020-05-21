using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
    public interface ITweetService
    {
        public void Add(TweetDTO model, string userName, string content, IFormFile image);
        public void Delete(Guid Id);
        public void Delete(Tweet tweet);
        public IList<TweetDTO> GetTweets();
        public IList<TweetDTO> GetByUsers(string userId);
        public IList<TweetDTO> GetByFollowed(string userName);
        public TweetDTO Get(Guid Id);
        
    }
}
