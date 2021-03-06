﻿using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
   public interface IAppUserService
    {
        public void Delete(AppUser user);
        public void Delete(string Id);
        public IList<UserDTO> GetByLike(Guid Id);
        public IList<UserDTO> GetByRetweet(Guid Id);
        public IList<UserDTO> GetFollowed(string userName, int? sayfano, int pageSize);
        public IList<UserDTO> GetFollower(string userName, int? sayfano, int pageSize);
        public IList<UserDTO> GetList();
        public IList<UserDTO> GetOnlineFriends(string userName, int? sayfano, int pageSize);
        public UserDTO GetById(string Id);
        public void Update(UserDTO model);
        public UserDTO GetByUserName(string userName);
        bool TakipEdiyorMu(string userName, string userName2);
        public IList<UserDTO> SearchList(string userName, int? sayfano, int pagesize);


    }
}
