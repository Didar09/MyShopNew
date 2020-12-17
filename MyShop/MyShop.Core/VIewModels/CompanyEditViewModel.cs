using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.VIewModels
{
    public class CompanyEditViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "CompanyName Required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Contact Person Name Required")]
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [Required(ErrorMessage = "Contact Person Phone Number is needed.")]
        [Display(Name = "Contact Person Moblie No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string ContactPersonMobile { get; set; }

        [Required(ErrorMessage = "Contact Person Email Required")]
        [Display(Name = "Contact Person Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactPersonEmail { get; set; }

        [Required(ErrorMessage = "Company Address required")]
        [DisplayName("Company Address")]
        public string CompanyAddress { get; set; }
        [Required(ErrorMessage = "Company Phone Number is needed.")]
        [Display(Name = "Company Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string CompanyPhone { get; set; }
        [Required(ErrorMessage = "State not Selected")]
        [DisplayName("State")]
        public string CompanyState { get; set; }
        [Required(ErrorMessage = "Country Required")]
        [DisplayName("Country")]
        public string Country { get; set; }

        public CompanyEditViewModel()
        {

        }
        public CompanyEditViewModel(Company company)
        {
            this.CompanyAddress = company.CompanyAddress;
            this.CompanyName = company.CompanyName;
            this.CompanyPhone = company.CompanyPhone;
            this.ContactPersonEmail = company.ContactPersonEmail;
            this.ContactPersonName = company.ContactPersonName;
            this.ContactPersonMobile = company.ContactPersonPMobile;
            this.CompanyState = company.StatesId;
            this.Country = company.CountryId;
            this.Id = company.Id;
        }
    }
}
