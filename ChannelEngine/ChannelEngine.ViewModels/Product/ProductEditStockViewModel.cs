using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.ViewModels.Product
{
    public class ProductEditStockViewModel
    {
        [Required]
        public string MerchantProductNo { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
