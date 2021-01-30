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
       
        public Admin(UserManager<AppUser> Usermanager, SignInManager<AppUser> SignInManager) : base(Usermanager, SignInManager)
        {
           // this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(Usermanager.Users.ToList());
        }

    }
}
