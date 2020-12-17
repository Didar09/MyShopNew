using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class States:BaseEntity
    {
        [StringLength(450)]
        public string State { get; set; }
    }
}
