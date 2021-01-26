using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.Models
{
    public class LoginViewModel
    {
        [Display(Name ="Email Adress")]
        [Required(ErrorMessage ="error")]
        [EmailAddress]
        public string Email { get; set; }
        [Display]
        [Required(ErrorMessage ="mesaj")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
