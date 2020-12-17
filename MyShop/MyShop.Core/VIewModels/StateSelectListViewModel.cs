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
    public class StateSelectListViewModel
    {
        public Company Company { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public StateSelectListViewModel(Company company, IEnumerable states)
        {
            Company = company;
            States = new SelectList(states, "States", "CName", company.States);
        }
    }
}
