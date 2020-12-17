using MyShop.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyShop.Core.VIewModels
{
    public class CountrySelectListViewModel
    {
        public Company Company { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        public CountrySelectListViewModel(Company company, IEnumerable country)
        {
            Company = company;
            Country = new SelectList(country, "CountryName", "CName", company.Countries.Id);
        }
    }
}
