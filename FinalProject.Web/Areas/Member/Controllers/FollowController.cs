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
    public class FollowController : Controller
    {
        private IFollowService _followService;
        private IAppUserService _appUserService;
        public FollowController(IFollowService followService, IAppUserService appUserService)
        {
            _followService = followService;
            _appUserService = appUserService;
        }
        //JSON
        public IActionResult Follow(string Id)
        {
            return new JsonResult(_followService.Follow(Id, User.Identity.Name));
        }

        public IActionResult GetFollower(string userName)
        {
            var model = _appUserService.GetFollower(userName);
            foreach (var item in model)
            {
                var result = _appUserService.TakipEdiyorMu(item.UserName, User.Identity.Name);
                if (result == true)
                {
                    ViewBag.Follow = "Unfollow";
                }
                else
                {
                    ViewBag.Follow = "Unfollow";
                }
            }
            return View(model);
        }
        public IActionResult GetFollowed(string userName)
        {
            return View(_appUserService.GetFollowed(userName));
        }
    }
}
