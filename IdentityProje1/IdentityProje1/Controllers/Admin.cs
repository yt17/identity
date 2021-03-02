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

        public IActionResult ListRole()
        {
            List<AppRole> roles = RoleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult DeleteRole(string ID)
        {
            AppRole role =RoleManager.FindByIdAsync(ID).Result;
            if (role!=null)
            {
                IdentityResult result = RoleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Admin");
                }
                else
                {
                    ViewBag.msj = "hata var";
                    AddErrors(result);
                    return RedirectToAction("ListRole", "Admin");
                }

            }
            else
            {
                return RedirectToAction("ListRole", "Admin");
            }

            
        }

        public IActionResult UpdateRole(string ID)
        {
            AppRole role = RoleManager.FindByIdAsync(ID).Result;
            RoleViewModel model = new RoleViewModel();
            role.Id = role.Id;
            role.Name = role.Name;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole role = RoleManager.FindByIdAsync(model.ID).Result;
                if (role!=null)
                {
                    role.Name = model.Name;
                    IdentityResult result = RoleManager.UpdateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRole", "Admin");
                    }
                    else
                    {
                        AddErrors(result);
                        return View(model);
                    }
                }
                else
                {                    
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult UserRoleAtttemption(string UserId)
        {
            AppUser appUser = Usermanager.FindByIdAsync(UserId).Result;
            ViewBag.UserName = appUser.UserName;

            IQueryable<AppRole> roles = RoleManager.Roles;

            List<string> UserRoles = Usermanager.GetRolesAsync(appUser).Result as List<string>;

            List<UserRoleViewModel> roleViewModels = new List<UserRoleViewModel>();

            foreach (var role in roles)
            {
                UserRoleViewModel usrvm = new UserRoleViewModel();
                usrvm.RoleID = role.Id;
                usrvm.RoleName = role.Name;
                if (UserRoles.Contains(role.Name))
                {
                    usrvm.exist = true;
                }
                else
                {
                    usrvm.exist = false;
                }
                roleViewModels.Add(usrvm);
            }
            //List<string> Roles=RoleManager.GetRoleIdAsync(RoleId).Result as List<string>();
            return View(roleViewModels);
        }

        public IActionResult Uyeler()
        {
            List<UsersModel> liste = Usermanager.Users.Select(w => new UsersModel
            {
                Email = w.Email,
                UserName=w.UserName,
                UserID=w.Id
            }).ToList();
            return View(liste);
        }

        public IActionResult RoleVer(string ID) 
        {
            ViewBag.User = ID;
            List<RoleViewModel> roller = RoleManager.Roles.Select(w => new RoleViewModel
            {
                ID = w.Id,
                Name = w.Name                
            }).ToList();
            
            return View(roller);
        }
        [HttpPost]
        public async Task<IActionResult> RoleVer(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var val = ViewBag.User;
                AppUser user = await Usermanager.FindByIdAsync(model.Name);
                //AppRole role = RoleManager.GetRoleIdAsync().Result;
                AppRole role = RoleManager.FindByIdAsync(model.ID).Result;
                IdentityResult res = Usermanager.AddToRoleAsync(user,role.Name.ToString()).Result;
                //IdentityResult res = RoleManager.SetRoleNameAsync(role, user.Id).Result;
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    AddErrors(res);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
            

        

    }
}
