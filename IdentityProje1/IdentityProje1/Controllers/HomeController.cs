using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityProje1.Models;
using Microsoft.AspNetCore.Identity;
using IdentityProje1.Helper;

namespace IdentityProje1.Controllers
{
    public class HomeController : Base
    {
        //private UserManager<AppUser> Usermanager { get; }
        //private SignInManager<AppUser> SignInManager { get; }
        
        //private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<AppUser> Usermanager,SignInManager<AppUser> SignInManager) :base(Usermanager, SignInManager)
        {
          //  _logger = logger;
            //SignInManager = signInManager;
            //this.Usermanager = Usermanager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Member", "Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //AppUser user = await Usermanager.FindByEmailAsync(model.Email);
                AppUser user = CurrentUser;
                if (user!=null)
                {
                    if (await Usermanager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "hesap kilitli");
                    }

                    await SignInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, model.Password, false,false);
                    if (result.Succeeded)
                    {
                        await Usermanager.ResetAccessFailedCountAsync(user);
                        if (TempData["ReturnUrl"]!=null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Member");
                    }
                    else
                    {
                        await Usermanager.AccessFailedAsync(user);

                        int fail = await Usermanager.GetAccessFailedCountAsync(user);
                        ModelState.AddModelError("", $"{fail} kez basarisiz giris");
                        if (fail==3)
                        {
                            await Usermanager.SetLockoutEndDateAsync(user,new System.DateTimeOffset(DateTime.Now.AddMinutes(20)));
                            ModelState.AddModelError("", "ban for 20 minutes");
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(model.Email), "gecersiz mail veya sifre");
                        }
                    }
                }
                else
                {
                    //return RedirectToAction()
                    ModelState.AddModelError(nameof(model.Email), "gecersiz mail veya sifre");
                }

            }
            return View();
        }

        public IActionResult SingUP()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SingUP(SignUp Model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = Model.UserName;
                user.PhoneNumber = Model.PhoneNumber;
                user.Email = Model.Email;

                IdentityResult identityResult=await Usermanager.CreateAsync(user, Model.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    AddErrors(identityResult);
                }
            }
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult ResetPassword(PasswordViewModel model)
        {
            //AppUser user = Usermanager.FindByEmailAsync(model.Email).Result;
            AppUser user = CurrentUser;
            if (user!=null)
            {
                string passwordResettoken = Usermanager.GeneratePasswordResetTokenAsync(user).Result;
                string passwrodlink = Url.Action("ResetPasswordConfirm", "Home", new
                {
                    userid=user.Id,
                    token=passwordResettoken

                },HttpContext.Request.Scheme);

                MailHelper.SendMail(passwrodlink,model.Email,"mmesaj'",false);

                ViewBag.status = "tmaam";

            }
            else
            {
                ModelState.AddModelError("", "boyle biri yok");
            }
            return View(model);
        }

        public IActionResult ResetPasswordConfirm(string userid,string token)
        {
            TempData["token"]= token;
            TempData["userid"] = userid;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(PasswordVM model)
        {
            string token = TempData["token"].ToString();
            string id = TempData["userid"].ToString();

            AppUser user = await Usermanager.FindByIdAsync(id);
            if (user!=null)
            {
                IdentityResult result = await Usermanager.ResetPasswordAsync(user,token,model.Password);
                if (result.Succeeded)
                {
                    await Usermanager.UpdateSecurityStampAsync(user);
                    TempData["passwordResetInfo"] = "sifreniz basariyla yenilendi";
                }
                else
                {
                    AddErrors(result);
                }

            }
            else
            {
                ModelState.AddModelError("", "boyle biri yok");
            }
            return View();
        }

    }
}
