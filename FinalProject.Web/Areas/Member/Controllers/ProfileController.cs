using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class ProfileController : Controller
    {
        int PageSize = 10;
        private IAppUserService _userService;
        private IRetweetService _retweetService;
        private ITweetService _tweetService;
        public ProfileController(IAppUserService userService, IRetweetService retweetService, ITweetService tweetService)
        {
            _userService = userService;
            _retweetService = retweetService;
            _tweetService = tweetService;
        }
        public IActionResult Profile(string userName,TweetUserVM model)
        {
            var user = _userService.GetByUserName(userName);
            var result = _userService.TakipEdiyorMu(userName, User.Identity.Name);
            if (result == true)
            {
                ViewBag.Follow = "UnFollow";
            }
            else
            {
                ViewBag.Follow = "Follow";
            }
            model.Retweets = _retweetService.GetByUser(user.Id).Take(PageSize).ToList();
            model.Tweets = _tweetService.GetByUsers(user.Id).Take(PageSize).ToList();
            model.User = _userService.GetByUserName(user.UserName);
            return View(model);
        }
    }
}