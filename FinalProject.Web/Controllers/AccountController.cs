using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.Associate.VM;
using FinalProject.Entities.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace FinalProject.Web.Controllers
{
    public class AccountController : Controller
    {
   
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IMapper _mapper)
        {
           
            userManager = _userManager;
            signInManager = _signInManager;
        
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                Redirect("/Admin/Home");
            }
            else if (User.Identity.IsAuthenticated)
            {
                Redirect("/Member/Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        user.OnlineMi = true;
                        await userManager.UpdateAsync(user);
                        return Redirect("/Member/Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please check your information");
                        return View(login);
                    }
                }
            }
            return View(login);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.InjectFrom(model);
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
               if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(" ", item.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult AccesDenied()
        {
            return View();
        }
        public  IActionResult GoogleLogin(string ReturnUrl)
        {
            string redirectUrl = Url.Action("ExternalResponse", "Account", new { ReturnUrl = ReturnUrl });
            //Google'a yapılan Login talebi neticesinde kullanıcıyı yönlendirmesini istediğimiz url'i oluşturuyoruz.
            AuthenticationProperties properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            //Bağlantı kurulacak harici platformun hangisi olduğunu belirtiyor ve bağlantı özelliklerini elde ediyoruz.
            return new ChallengeResult("Google", properties);
             //ChallengeResult; kimlik doğrulamak için gerekli olan tüm özellikleri kapsayan AuthenticationProperties nesnesini alır ve ayarlar.
        }
        public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
        {
            ExternalLoginInfo loginInfo = await signInManager.GetExternalLoginInfoAsync();
            
            if (loginInfo == null)
                return RedirectToAction("Login");
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult loginResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
            
                if (loginResult.Succeeded)
                    return Redirect("/Member/Home");
                else
                {
                    
                    AppUser user = new AppUser
                    {
                        Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value
                    };
                  
                    IdentityResult createResult = await userManager.CreateAsync(user);
                   
                    if (createResult.Succeeded)
                    {
                       
                        IdentityResult addLoginResult = await userManager.AddLoginAsync(user, loginInfo);
                       
                        if (addLoginResult.Succeeded)
                        {
                            await signInManager.SignInAsync(user, true);
                           
                            return Redirect(ReturnUrl);
                        }
                    }

                }
            }
            return Redirect(ReturnUrl);
        }

    }
}