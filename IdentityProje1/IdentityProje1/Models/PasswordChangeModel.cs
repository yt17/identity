using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProje1.Models
{
    public class PasswordChangeModel
    {
        [Display(Name ="Eski Sifre")]
        [Required(ErrorMessage = "mesaj")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string PasswordOld { get; set; }
        [Display(Name = "yeni sifre")]
        [Required(ErrorMessage = "mesaj")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string PasswordNew { get; set; }
        [Display(Name = "confirm")]
        [Required(ErrorMessage = "mesaj")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string PasswordConfirm { get; set; }
    }
}
