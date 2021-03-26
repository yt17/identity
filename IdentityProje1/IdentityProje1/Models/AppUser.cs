using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.Models
{
    public class AppUser:IdentityUser
    {
        public string City { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
