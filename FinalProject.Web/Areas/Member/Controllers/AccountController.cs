using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Business.ValueInjecter;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace FinalProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class AccountController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private IUnitOfWork uow;
        private IMapper mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IPasswordHasher<AppUser> _passwordHasher;
        public AccountController(IPasswordHasher<AppUser> passwordHasher, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper _mapper, IUnitOfWork _uow, IWebHostEnvironment _webHostEnvironment)
        {

            uow = _uow;
            mapper = _mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            webHostEnvironment = _webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            UserDTO model = new UserDTO();
            model.InjectFrom(user);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO model)
        {
            AppUser user = await _userManager.FindByIdAsync(model.Id);
            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "media/user");

                    model.ImagePath = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, model.ImagePath);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await model.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }
                else
                {
                    model.ImagePath = user.ImagePath;
                }
                user.InjectFrom<FilterId>(model);
                if (model.Password != null)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                }
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    user.OnlineMi = false;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.SignOutAsync();
                    return Redirect("/Account/Login");
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
        public async Task<IActionResult> Logout(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _signInManager.SignOutAsync();
            user.OnlineMi = false;
            await _userManager.UpdateAsync(user);
            return Redirect("/Account/Login");
        }
    }
}