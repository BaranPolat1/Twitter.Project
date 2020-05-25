using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private IPasswordHasher<AppUser> passwordHasher;
        private UserManager<AppUser> userManager;
        private IMapper mapper;
        private IUnitOfWork uow;
        public AccountController(IMapper _mapper, UserManager<AppUser> _userManager, IUnitOfWork _uow, IPasswordHasher<AppUser> _passwordHasher, IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            passwordHasher = _passwordHasher;
            uow = _uow;
            userManager = _userManager;
            mapper = _mapper;
        }


        public async Task<IActionResult> Delete(string Id)
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    uow.SaveChange();
                    RedirectToAction("List");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return RedirectToAction("List");
        }


        public IActionResult List()
        {
            var item = uow.User.GetAll();
            var model = mapper.Map<List<UserDTO>>(item);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            UserDTO model = mapper.Map<UserDTO>(user);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO model)
        {
            AppUser user = await userManager.FindByIdAsync(model.Id);
            if (ModelState.IsValid)
            {
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                user.Email = model.Email;
                string imageName = null;
                if (model.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "media/user");
                    imageName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await model.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }
                model.ImagePath = imageName;
                user.ImagePath = model.ImagePath;
                if (model.Password != null)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                }
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    uow.SaveChange();
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(user);
        }
    }
}
