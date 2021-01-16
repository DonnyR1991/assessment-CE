using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.ViewModels.Product
{
    public class ProductViewModel
    {
        public string MerchantProductNo { get; set; }
        public string Description { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
    }
}
