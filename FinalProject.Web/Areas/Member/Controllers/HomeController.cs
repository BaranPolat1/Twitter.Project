using System;
using System.Collections.Generic;
using System.IO;
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
    public class HomeController : Controller
    {
        private ITweetService _tweetService;
        private IRetweetService _retweetService;
        private IAppUserService _appUserService;
        public HomeController(ITweetService tweetService,
         IRetweetService retweetService,IAppUserService appUserService)
        {
            _appUserService = appUserService;
            _tweetService = tweetService;
            _retweetService = retweetService;
        }
        public IActionResult Index(TweetVM model)
        {
            var user = _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Image = Path.GetFileName(user.ImagePath);
            model.Tweets = _tweetService.GetByFollowed(User.Identity.Name);
            model.Retweets = _retweetService.GetByFollowed(User.Identity.Name);
            return View(model);
        }
       
        public IActionResult OnlineFriends()
        {
            var model = _appUserService.GetOnlineFriends(User.Identity.Name);
            return View(model);
        }
    }
}