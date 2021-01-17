using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityProje1.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProje1.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> Usermanager { get; }
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> Usermanager)
        {
            _logger = logger;
            this.Usermanager = Usermanager;
        }

        public IActionResult Index()
        {
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

        public IActionResult Login()
        {
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
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }

    }
}
