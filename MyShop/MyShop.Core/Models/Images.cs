using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Models
{
    public class ImagePath:BaseEntity
    {
        public static Stream InputStream { get; set; }
        public string Image { get; set; }
        [DisplayName("Upload File")]
        public string ImagesPath { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
