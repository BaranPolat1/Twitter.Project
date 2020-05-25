using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class SearchController : Controller
    {
        int pageSize = 15;
        private IUnitOfWork _db;
        private IAppUserService _user;
        public SearchController(IUnitOfWork db, IAppUserService user)
        {
            _user = user;
            _db = db;
           
        }

        public IActionResult SearchUser(string term)
        {
            var users = _db.User.FindByList(x=>x.UserName.StartsWith(term)).Take(pageSize);
            var model = from c in users
                        select new
                        {
                            id = c.Id,
                            value = c.UserName,
                            url = "/Member/Profile/Profile?UserName=" + c.UserName
                        };
            return new JsonResult(model);
        }
        public IActionResult SearchList(string userName,int? sayfano)
        {
            if (userName == null)
            {
                return NotFound();
            }
            var model = _user.SearchList(userName, sayfano, pageSize);
            TempData["UserName"] = userName;
            bool isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_UserListPartial", model);
            }
            return View(model);
        }
    }
}