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
    public class CompanyViewModel
    {
        [Required(ErrorMessage = "CompanyName is Required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Contact Person Name is Required")]
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [Required(ErrorMessage = "Contact Person Phone Number is needed.")]
        [Display(Name = "Contact Person Moblie No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string ContactPersonMobile { get; set; }

        [Required(ErrorMessage = "Contact Person Email is Required")]
        [Display(Name = "Contact Person Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactPersonEmail { get; set; }

        [Required(ErrorMessage = "Company Address is required")]
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
        [Required(ErrorMessage = "Country not Selected")]
        [DisplayName("Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and Must Contain : upper case, lower case , number and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
        public CompanyViewModel()
        {

        }

        public CompanyViewModel(Company company)
        {
            this.CompanyAddress = company.CompanyAddress;
            this.CompanyName = company.CompanyName;
            this.CompanyPhone = company.CompanyPhone;
            this.ContactPersonEmail = company.ContactPersonEmail;
            this.ContactPersonName = company.ContactPersonName;
            this.ContactPersonMobile = company.ContactPersonPMobile;
            this.Password = company.Password;
            this.IsActive = company.IsActive;
            this.CompanyState = company.StatesId;
            this.Country = company.CountryId;
        }
       // public IEnumerable<Company> Companies { get; set; }
    }
}
