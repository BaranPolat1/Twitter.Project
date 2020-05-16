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
    public class RetweetController : Controller
    {
        private IAppUserService _userService;
        private IRetweetService _retweetService;
        public RetweetController( IRetweetService retweetService, IAppUserService userService)
        {
            _retweetService = retweetService;
            _userService = userService;
       
        }
        //JSON
        public  IActionResult AddRetweet(Guid Id)
        {
            return new JsonResult(_retweetService.Retweet(Id, User.Identity.Name));
       }
        public IActionResult Show (Guid Id)
        {
            return View(_userService.GetByRetweet(Id));
        }

       
    }
}