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
        int pageSize = 30;
        private ITweetService _tweetService;
        private IRetweetService _retweetService;
        private IAppUserService _appUserService;
        public HomeController(ITweetService tweetService,
         IRetweetService retweetService, IAppUserService appUserService, IUnitOfWork unitOfWork)
        {
            _appUserService = appUserService;
            _tweetService = tweetService;
            _retweetService = retweetService;

        }
        public IActionResult OnlineFriends(int? sayfano)
        {
            ViewBag.UserName = User.Identity.Name;
            var model = _appUserService.GetOnlineFriends(User.Identity.Name, sayfano, pageSize);
            bool isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_UserListPartial", model);
            }
            return View(model);
        }
        public IActionResult Index(TweetUserVM model)
        {
            var user = _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Image = Path.GetFileName(user.ImagePath);
            model.Tweets = _tweetService.GetByFollowed(User.Identity.Name).Take(pageSize).ToList();
            model.Retweets = _retweetService.GetByFollowed(User.Identity.Name).Take(pageSize).ToList();
            return View(model);
        }
    }
}