using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.Models
{
    public class PasswordViewModel
    {
        [Display(Name = "Email Adress")]
        [Required(ErrorMessage = "error")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
