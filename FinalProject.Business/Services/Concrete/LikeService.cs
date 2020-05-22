using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{
    public class LikeService : ILikeService
    {
        private IUnitOfWork _uow;
        public LikeService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public JsonLikeVM Like(Guid Id, string userName)
        {
            JsonLikeVM js = new JsonLikeVM();
            Tweet tweet = _uow.Tweet.GetById(Id);
            AppUser user = _uow.User.Find(x => x.UserName == userName) ;
            if (!(_uow.Like.Any(x => x.UserId == user.Id && x.TweetId == tweet.Id)))
            {
                try
                {
                    Like like = new Like();
                    like.TweetId = tweet.Id;
                    like.UserId = user.Id;
                    _uow.Like.Add(like);
                    _uow.SaveChange();
                    js.likes = _uow.Like.FindByList(x => x.TweetId == tweet.Id).Count();
                    return js;
                }
                catch 
                {
                    js.likes = 0;
                    return js;
                   
                }
                
            }
            else
            {
                Like like = _uow.Like.Find(x => x.TweetId == tweet.Id && x.UserId == user.Id);
                if (like != null)
                {
                    _uow.Like.Delete(like);
                    _uow.SaveChange();
                    js.likes = _uow.Like.FindByList(x => x.TweetId == tweet.Id).Count();
                }
                else
                {
                    js.likes = 0;
                }
                
                return js;
            }
        }
    }
}
