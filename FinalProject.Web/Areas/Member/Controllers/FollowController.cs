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
using X.PagedList;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class FollowController : Controller
    {
        int pageSize = 5;
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

        public IActionResult GetFollower(string userName,int? sayfano)
        {
            ViewBag.UserName = userName;
            var model = _appUserService.GetFollower(userName,sayfano,pageSize);
            bool isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_UserListPartial", model);
            }
            return View(model);
        }
        public IActionResult GetFollowed(string userName,int? sayfano)
        {
            ViewBag.UserName = userName;
            var model = _appUserService.GetFollowed(userName,sayfano,pageSize);
            bool isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_UserListPartial", model);
            }
            return View(model);
        }
    }
}
