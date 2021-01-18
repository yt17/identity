using IdentityProje1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PasswordContains", Description = "description" });
            }
            if (password.ToLower().Contains("1234"))
            {
                errors.Add(new IdentityError() { Code = "PasswordContains1234", Description = "description" });
            }
            if (errors.Count==0)
            {
                return Task.FromResult(IdentityResult.Success);                 
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            throw new NotImplementedException();
        }
    }
}
