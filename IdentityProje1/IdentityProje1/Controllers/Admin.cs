using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProje1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProje1.Controllers
{
    public class Admin : Base
    {
       
        public Admin(UserManager<AppUser> Usermanager, SignInManager<AppUser> SignInManager, RoleManager<AppRole> RoleManager) : base(Usermanager,SignInManager,RoleManager)
        {
           // this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(Usermanager.Users.ToList());
        }
        public IActionResult RoleEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleEkle(RoleEkle roleEkle)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole();
                role.Name = roleEkle.RoleName;
                IdentityResult result =RoleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    AddErrors(result);
                    return View(roleEkle);
                }
            }
            else
            {
                return View(roleEkle);
            }       
        }

    }
}
