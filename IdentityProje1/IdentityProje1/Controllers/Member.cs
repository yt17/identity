using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProje1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mapster;
namespace IdentityProje1.Controllers
{
    [Authorize]
    public class Member : Controller
    {
        private UserManager<AppUser> Usermanager { get; }
        private SignInManager<AppUser> SignInManager { get; }
        public Member(UserManager<AppUser> Usermanager, SignInManager<AppUser> SignInManager)
        {
            this.Usermanager = Usermanager;
            this.SignInManager = SignInManager;
        }


        public IActionResult Index()
        {
            AppUser user = Usermanager.FindByNameAsync(User.Identity.Name).Result;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();
           // userViewModel.Name = user.UserName;
           

            
            return View(userViewModel);
        }
    }
}
