﻿using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Business.ValueInjecter;
using FinalProject.Entities.Entity;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        public AppUserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }
        public void Delete(AppUser user)
        {
            _uow.User.Delete(user);
            _uow.SaveChange();
        }

        public void Delete(string Id)
        {
            var user = _uow.User.Find(x => x.Id == Id);
            _uow.User.Delete(user);
            _uow.SaveChange();
        }
        public UserDTO GetById(string Id)
        {
            var user = _uow.User.Find(x => x.Id == Id);
            UserDTO model = _mapper.Map<UserDTO>(user);
            return model;
        }
        //seçilen bir tweeti beğenen kullanıcıların listesi.
        public IList<UserDTO> GetByLike(Guid Id)
        {
            Tweet tweet = _uow.Tweet.GetById(Id);
            var like = _uow.Like.FindByList(x => x.TweetId == tweet.Id);
            List<AppUser> users = new List<AppUser>();
            foreach (var item in like)
            {
                users.AddRange(_uow.User.FindByList(X => X.Id == item.UserId));
            }
            var model = _mapper.Map<IList<UserDTO>>(users);
            return model;

        }

        //seçilen bir tweeti retweet yapan kullanıcıların listesi
        public IList<UserDTO> GetByRetweet(Guid Id)
        {
            Tweet tweet = _uow.Tweet.GetById(Id);
            var retweet = _uow.Retweet.FindByList(x => x.TweetId == tweet.Id);
            List<AppUser> appUsers = new List<AppUser>();
            foreach (var item in retweet)
            {
                appUsers.AddRange(_uow.User.FindByList(x => x.Id == item.UserId));
            }
            var model = _mapper.Map<List<UserDTO>>(appUsers);
            return model;
        }

        public UserDTO GetByUserName(string userName)
        {
            var user = _uow.User.Find(x => x.UserName == userName);
            UserDTO model = _mapper.Map<UserDTO>(user);
            return model;
        }

        public IList<UserDTO> GetFollowed(string userName, int? sayfano, int pageSize)
        {
            IList<UserDTO> model = null;
            var users = _uow.User.Find(x => x.UserName == userName);
            var followed = _uow.Follow.FindByList(x => x.FollowerId == users.Id);
            List<AppUser> followedList = new List<AppUser>();

            foreach (var item in followed)
            {
                followedList.AddRange(_uow.User.FindByList(x => x.Id == item.FollowedId));

            }
            if (sayfano == null)
            {
                model = _mapper.Map<IList<UserDTO>>(followedList).OrderBy(X => X.UserName).Take(pageSize).ToList();
            }
            else
            {
                model = _mapper.Map<IList<UserDTO>>(followedList).OrderBy(X => X.UserName).Skip(pageSize * sayfano.Value).Take(pageSize).ToList();
            }

            return model;
        }

        public IList<UserDTO> GetFollower(string userName, int? sayfano, int pageSize)
        {
            IList<UserDTO> model = null;
            var users = _uow.User.Find(x => x.UserName == userName);
            var followed = _uow.Follow.FindByList(x => x.FollowedId == users.Id);
            List<AppUser> followerList = new List<AppUser>();
            foreach (var item in followed)
            {
                followerList.AddRange(_uow.User.FindByList(x => x.Id == item.FollowerId));
            }
            if (sayfano == null)
            {
                model = _mapper.Map<IList<UserDTO>>(followerList).OrderBy(X => X.UserName).Take(pageSize).ToList();
            }
            else
            {
                model = _mapper.Map<IList<UserDTO>>(followerList).OrderBy(X => X.UserName).Skip(pageSize * sayfano.Value).Take(pageSize).ToList();
            }
            return model;
        }

        public IList<UserDTO> GetList()
        {
            var users = _uow.User.GetAll();
            IList<UserDTO> model = _mapper.Map<IList<UserDTO>>(users);
            return model;
        }

        public IList<UserDTO> GetOnlineFriends(string userName,int? sayfano,int pageSize)
        {
            var users = _uow.User.Find(x => x.UserName == userName);
            var followed = _uow.Follow.FindByList(x => x.FollowerId == users.Id);
            IList<UserDTO> model = null;
            List<AppUser> followedList = new List<AppUser>();
            List<AppUser> online = new List<AppUser>();
            foreach (var item in followed)
            {
                followedList.AddRange(_uow.User.FindByList(x => x.Id == item.FollowedId));

            }
            foreach (var item in followedList)
            {
                online.AddRange(_uow.User.FindByList(x => x.Id == item.Id && x.OnlineMi == true));
            }
            if (sayfano == null)
            {
                model = _mapper.Map<IList<UserDTO>>(online).OrderBy(x=>x.UserName).Take(pageSize).ToList();
            }
            else
            {
                model = _mapper.Map<IList<UserDTO>>(online).OrderBy(x => x.UserName).Skip(pageSize * sayfano.Value).Take(pageSize).ToList();
            }
            return model;
        }

        public bool TakipEdiyorMu(string userName, string userName2)
        {
            var user = _uow.User.Find(x => x.UserName == userName);
            var user2 = _uow.User.Find(x => x.UserName == userName2);
            var follower = _uow.Follow.FindByList(x => x.FollowedId == user.Id);
            if (follower.Any(x => x.FollowerId == user2.Id))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Update(UserDTO model)
        {
            var user = _uow.User.Find(x => x.Id == model.Id);
            user.InjectFrom<FilterId>(model);
            _uow.User.Update(user);
            _uow.SaveChange();

        }
 
        public IList<UserDTO> SearchList(string userName, int? sayfano, int pagesize)
        {
            IList<UserDTO> model = null;
            var users = from u in _uow.User.GetAll()
                        select u;

            if (sayfano == null)
            {
                if (!String.IsNullOrEmpty(userName))
                {
                    users = users.Where(s => s.UserName.Contains(userName)).OrderBy
                        (x => x.UserName).Take(pagesize).ToList();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(userName))
                {
                    users = users.Where(s => s.UserName.Contains(userName)).OrderBy(x => x.UserName).Skip(pagesize * sayfano.Value).Take(pagesize).ToList();
                }
            }

            model = _mapper.Map<IList<UserDTO>>(users);

            return model;
        }

    }
}
