using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{
    public class TweetService : ITweetService
    {
        private IMapper _mapper;
        private IUnitOfWork _uow;
        private IWebHostEnvironment _environment;
        public TweetService(IUnitOfWork uow, IWebHostEnvironment environment, IMapper mapper)
        {
            _uow = uow;
            _environment = environment;
            _mapper = mapper;
        }
        public void Add(TweetDTO model, string userName, string content, IFormFile image)
        {
            var user = _uow.User.Find(x => x.UserName == userName);
            model.Content = content;
            model.UserId = user.Id;
            if (image != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "media/tweet");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }
                string fileName = Path.GetFileName(image.FileName);
                using (FileStream stream = new FileStream(Path.Combine(uploadDir, fileName), FileMode.Create))
                {
                    image.CopyTo(stream);
                    model.ImagePath = fileName;
                }
            }
            Tweet tweet = _mapper.Map<Tweet>(model);
            tweet.CreatedDate = DateTime.Now;
            _uow.Tweet.Add(tweet);
            _uow.SaveChange();
        }
        public void Delete(Guid Id)
        {
            var tweet = _uow.Tweet.GetById(Id);
            if (tweet != null)
            {
                _uow.Tweet.Delete(tweet);
                _uow.SaveChange();
            }
        }

        public void Delete(Tweet tweet)
        {
            if (tweet != null)
            {
                _uow.Tweet.Delete(tweet);
                _uow.SaveChange();
            }
        }

        public TweetDTO Get(Guid Id)
        {
            var tweet = _uow.Tweet.GetById(Id);
            try
            {
                TweetDTO model = _mapper.Map<TweetDTO>(tweet);
                return model;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public IList<TweetDTO> GetByFollowed(string userName)
        {
            var user = _uow.User.Find(x => x.UserName == userName);
            var followed = _uow.Follow.FindByList(x => x.FollowerId == user.Id);
            List<Tweet> tweets = new List<Tweet>();
            foreach (var item in followed)
            {
                tweets.AddRange(_uow.Tweet.FindByList(x => x.UserId == item.FollowedId));
            }
            tweets.AddRange(_uow.Tweet.FindByList(x => x.UserId == user.Id));
            var tweetList = tweets.OrderByDescending(x => x.CreatedDate);
            var model = _mapper.Map<IList<TweetDTO>>(tweetList);
            return model;
        }

        public IList<TweetDTO> GetByUsers(string userId)
        {
            var tweet = _uow.Tweet.FindByList(x => x.UserId == userId).OrderByDescending(x => x.CreatedDate); ;
            IList<TweetDTO> model = _mapper.Map<IList<TweetDTO>>(tweet);
            return model;
        }

        public IList<TweetDTO> GetTweets()
        {
            var tweets = _uow.Tweet.GetAll();
            IList<TweetDTO> model = _mapper.Map<IList<TweetDTO>>(tweets);
            return model;
        }
    }
}
