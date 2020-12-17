using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Country:BaseEntity
    {
        [StringLength(450)]
        public string CountryName { get; set; }
        public ICollection<States> States { get; set; }
    }
}
