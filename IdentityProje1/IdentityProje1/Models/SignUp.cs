using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.Models
{
    public class SignUp
    {
        [Required(ErrorMessage = "bos girilemez")]
        [Display(Name = "Kullanici adi")]
        public string UserName { get; set; }
        [Display(Name = "Tel no")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "gerekli")]
        [Display(Name = "mail")]
        [EmailAddress(ErrorMessage = "mail adresiniz dogru girin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "sifre gerekli")]
        [Display(Name = "sifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
