using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.VIewModels
{
    public class ProductImagesViewModel
    {
        public HttpPostedFileBase Image { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        [DisplayName("Upload")]
        public string ImagePath { get; set; }

        public List<string> ImageUrl { get; set; }

    }

}
