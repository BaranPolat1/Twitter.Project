using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{
    public class RetweetService : IRetweetService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        public RetweetService(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IList<RetweetDTO> GetByFollowed(string userName)
        {
            AppUser user = _uow.User.Find(x=>x.UserName == userName);
            var followed = _uow.Follow.FindByList(x => x.FollowerId == user.Id);
            List<Retweet> retweets = new List<Retweet>();
            foreach (var item in followed)
            {
                retweets.AddRange(_uow.Retweet.FindByList(x => x.UserId == item.FollowedId).OrderByDescending(x => x.CreatedDate).Take(10));
            }
            retweets.AddRange(_uow.Retweet.FindByList(x => x.UserId == user.Id).OrderByDescending(x => x.CreatedDate).Take(10));
            IList<RetweetDTO> model = _mapper.Map<IList<RetweetDTO>>(retweets);
            return model;
        }

        public IList<RetweetDTO> GetByTweet(Guid Id)
        {
            var retweet = _uow.Retweet.FindByList(x => x.TweetId == Id);
            IList<RetweetDTO> model = _mapper.Map<IList<RetweetDTO>>(retweet);
            return model;
        }

        public IList<RetweetDTO> GetByUser(string userId)
        {
            var retweet = _uow.Retweet.FindByList(x => x.UserId == userId);
            IList<RetweetDTO> model = _mapper.Map<IList<RetweetDTO>>(retweet);
            return model;
        }

        public JsonRetweetVM Retweet(Guid Id, string userName)
        {
            JsonRetweetVM js = new JsonRetweetVM();
            Tweet tweet = _uow.Tweet.GetById(Id);
            AppUser user = _uow.User.Find(x=>x.UserName == userName);
            if (!(_uow.Retweet.Any(x => x.UserId == user.Id && x.TweetId == tweet.Id)))
            {
                Retweet retweet = new Retweet();
                retweet.UserId = user.Id;
                retweet.TweetId = tweet.Id;
                _uow.Retweet.Add(retweet);
                _uow.SaveChange();
                js.retweets = _uow.Retweet.FindByList(x => x.TweetId == tweet.Id).Count();
                return js;
            }
            else
            {
                Retweet retweet = _uow.Retweet.Find(x => x.TweetId == tweet.Id && x.UserId == user.Id);
                _uow.Retweet.Delete(retweet);
                _uow.SaveChange();
                js.retweets = _uow.Retweet.FindByList(x => x.TweetId == tweet.Id).Count();
                return js;
            }
        }
    }
}
