using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class SearchController : Controller
    {
        private IMapper mapper;
        private ProjectContext _db;
        public SearchController(ProjectContext db, IMapper _mapper)
        {

            _db = db;
            mapper = _mapper;
        }

        public IActionResult SearchUser(string userName)
        {
            var users = from u in _db.Users
                        select u;
            if (!String.IsNullOrEmpty(userName))
            {
                users = users.Where(s => s.UserName.Contains(userName));
            }
           
            var model = mapper.Map<List<UserDTO>>(users);
            TempData["UserName"] = userName;
            return View(model);
        }
    }
}