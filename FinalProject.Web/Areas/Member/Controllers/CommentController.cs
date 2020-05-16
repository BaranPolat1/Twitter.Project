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
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public  IActionResult Add(CommentDTO model,string content,Guid tweetId)
        {
            _commentService.Add(model, content, User.Identity.Name, tweetId);
             return new JsonResult("");
        }
        public IActionResult Delete(Guid Id,Guid tweetId)
        {
            _commentService.Delete(Id);
            return Redirect($"/Member/Tweet/Show/{tweetId}");

        }
    }
}