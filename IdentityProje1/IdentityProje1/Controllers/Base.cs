using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProje1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProje1.Controllers
{
    public class Base : Controller
    {
        protected UserManager<AppUser> Usermanager { get; }
        protected SignInManager<AppUser> SignInManager { get; }
        public Base(UserManager<AppUser> Usermanager, SignInManager<AppUser> SignInManager)
        {
            this.Usermanager = Usermanager;
            this.SignInManager = SignInManager;
        }
        protected AppUser CurrentUser => Usermanager.FindByNameAsync(User.Identity.Name).Result;
        public void AddErrors(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

    }
}
