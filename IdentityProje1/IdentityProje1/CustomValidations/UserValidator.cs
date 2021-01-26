using IdentityProje1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            string[] digits = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            foreach (var item in digits)
            {
                if (user.UserName[0].ToString() == item)
                {
                    errors.Add(new IdentityError() { Code = "UserNameContainsfistletterdigitscontaions", Description = "Kullanici adinn ilk karakteri sayisal olamaz" });
                }
            }
            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
