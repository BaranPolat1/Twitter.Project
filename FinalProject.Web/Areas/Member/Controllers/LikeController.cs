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
using Newtonsoft.Json;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class LikeController : Controller
    {
        private ILikeService _likeService;
        private IAppUserService _appUserService;
             
        public LikeController(  ILikeService likeService,
         IAppUserService appUserService)
        {
            _likeService = likeService;
            _appUserService = appUserService;
        }
       
        //JSON
        public  IActionResult AddLike(Guid Id)
        {
            return new JsonResult(_likeService.Like(Id, User.Identity.Name));
        }
        public IActionResult Show(Guid Id)
        {
            return View(_appUserService.GetByLike(Id));
        }


       
       
    }
}