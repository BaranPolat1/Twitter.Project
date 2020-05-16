using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Business.UnitOfWork.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork uow;
        public SearchController(IUnitOfWork _uow, IMapper _mapper)
        {

            uow = _uow;
            mapper = _mapper;
        }

        public IActionResult SearchUser(string userName)
        {
            var users = from u in uow.User.GetAll()
                        select u;
            if (!String.IsNullOrEmpty(userName))
            {
                users = users.Where(s => s.UserName.Trim().Contains(userName)).ToList();
            }

            var model = mapper.Map<List<UserDTO>>(users);
            TempData["UserName"] = userName;
            return View(model);
        }
    }
}