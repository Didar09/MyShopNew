using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.VIewModels
{
    public class CompanyLoginViewModel
    {
        [Required(ErrorMessage = "Contact Person Email Required")]
        [Display(Name = "Contact Person Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactPersonEmail { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
