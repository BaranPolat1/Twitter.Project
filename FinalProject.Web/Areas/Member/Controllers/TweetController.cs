﻿using System;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class TweetController : Controller
    {
       
        private ITweetService _tweetService;
        private IRetweetService _retweetService;
        private ICommentService _commentService;
        public TweetController(ITweetService tweetService, IRetweetService retweetService, ICommentService commentService)
        {
            _tweetService = tweetService;
            _retweetService = retweetService;
            _commentService = commentService;
        }
        
        public IActionResult Add(TweetDTO model,string content,IFormFile file)
        {
            _tweetService.Add(model, User.Identity.Name, content, file);
            return new JsonResult("");
        }
        public IActionResult Delete(Guid Id)
        {
          
            return new JsonResult(_tweetService.Delete(Id));
        }
        public IActionResult Show(Guid Id)
        {
            TweetVM model = new TweetVM();
            model.Tweet = _tweetService.Get(Id);
            model.Comments = _commentService.GetByTweet(Id);
            model.Retweets = _retweetService.GetByTweet(Id);
            return View(model);
        }

    }
}