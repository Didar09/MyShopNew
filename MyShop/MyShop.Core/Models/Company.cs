using MyShop.Core.VIewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Company:BaseEntity
    {
        [StringLength(450)]
        public string CompanyName { get; set; }
        [StringLength(450)]
        public string ContactPersonName { get; set; }
        [StringLength(10)]
        public string ContactPersonPMobile { get; set; }
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string ContactPersonEmail { get; set; }
        [StringLength(450)]
        public string CompanyAddress { get; set; }
        [Index(IsUnique = true)]
        [StringLength(10)]
        public string CompanyPhone { get; set; }
        [StringLength(450)]
        public string Password { get; set; }
        public Role Roles { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("States")]
        public string StatesId { get; set; }
        public virtual States States { get; set; }

        [ForeignKey("Countries")]
        public string CountryId { get; set; }
        public virtual Country Countries { get; set; }
        public Company()
        {

        }

        public Company(CompanyViewModel vm)
        {
            this.CompanyAddress = vm.CompanyAddress;
            this.CompanyName = vm.CompanyName;
            this.CompanyPhone = vm.CompanyPhone;
            this.ContactPersonEmail = vm.ContactPersonEmail;
            this.ContactPersonName = vm.ContactPersonName;
            this.ContactPersonPMobile = vm.ContactPersonMobile;
            this.Password = vm.Password;
            this.IsActive = vm.IsActive;
            this.StatesId = vm.CompanyState;
            this.CountryId = vm.Country;
        }
        public Company(CompanyEditViewModel vem)
        {
            this.CompanyAddress = vem.CompanyAddress;
            this.CompanyName = vem.CompanyName;
            this.CompanyPhone = vem.CompanyPhone;
            this.ContactPersonEmail = vem.ContactPersonEmail;
            this.ContactPersonName = vem.ContactPersonName;
            this.ContactPersonPMobile = vem.ContactPersonMobile;
            this.StatesId = vem.CompanyState;
            this.CountryId = vem.Country;
        }
        public enum Role
        {
            User = 0,
            Admin = 1
        }
    }
}
