using IdentityProje1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityProje1.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {
        public ClaimProvider(UserManager<AppUser> Usermanager)
        {
            this.Usermanager = Usermanager;
        }

        public UserManager<AppUser> Usermanager { get; set; }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal != null && principal.Identity.IsAuthenticated == true)
            {
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
                AppUser user = await Usermanager.FindByNameAsync(identity.Name);
                if (user != null)
                {
                    if (user.City!=null)
                    {
                        if (user.BirthDate!=null)
                        {
                            var today = DateTime.Today;
                            var age = today.Year - user.BirthDate.Year;
                            if (age>15)
                            {
                                Claim claim = new Claim("Violance", true.ToString(), ClaimValueTypes.String, "Internal");
                                identity.AddClaim(claim);


                            }
                        }
                        


                        if (!principal.HasClaim(c=>c.Type=="City"))
                        {
                            Claim claim = new Claim("City", user.City, ClaimValueTypes.String, "Internal");
                            identity.AddClaim(claim);
                        }
                       
                    }
                }
            }
            return principal;
        }
    }
}
