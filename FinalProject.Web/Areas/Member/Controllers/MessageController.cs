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
    public class MessageController : Controller
    {
        private IMessageService _messageService;
        private IAppUserService _userService;
        public MessageController(IMessageService messageService, IAppUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult MessageBox()
        {
            var model = _messageService.GetOwnLastMessage(User.Identity.Name);
            return View(model);
        }
        public IActionResult ChatRoom(string userName)
        {
            var user = _userService.GetByUserName(userName);
            var user2 = _userService.GetByUserName(User.Identity.Name);
            ViewBag.RecipientId = user.Id;
            ViewBag.Image = Path.GetFileName(user2.ImagePath);
            return View(_messageService.GetChatRoom(User.Identity.Name,userName));
        }



    }
}