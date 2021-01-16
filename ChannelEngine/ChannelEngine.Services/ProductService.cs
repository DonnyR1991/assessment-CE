using ChannelEngine.Common;
using ChannelEngine.ViewModels.Order;
using ChannelEngine.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Services
{
    public class ProductService
    {
        private readonly OrderService _orderService;

        public ProductService(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ProductViewModel> GetByMerchantProductNo(string merchantProductNo)
        {
            var orders = await GetOrdersInProgress();

            var product = orders.Content
                .SelectMany(o => o.Lines)
                .FirstOrDefault(ol => ol.MerchantProductNo == merchantProductNo);

            if (product == null)
            {
                return null;
            }

            return new ProductViewModel()
            {
                MerchantProductNo = merchantProductNo,
                Description = product.Description,
                Gtin = product.Gtin,
                Quantity = product.Quantity
            };
        }

        public async Task<IEnumerable<ProductViewModel>> GetTopSoldProducts(int amount)
        {
            var orders = await GetOrdersInProgress();

            if (orders == null)
            {
                return null;
            }

            var products = orders.Content
                .SelectMany(o => o.Lines)
                .GroupBy(ol => ol.MerchantProductNo) // Is uniek denk ik?
                .Select(ol => new ProductViewModel
                {
                    MerchantProductNo = ol.Key,
                    Description = ol.FirstOrDefault().Description,
                    Gtin = ol.FirstOrDefault().Gtin,
                    Quantity = ol.Sum(x => x.Quantity)
                })
                .OrderByDescending(ol => ol.Quantity)
                .Take(amount);


            return products;
        }

        private async Task<OrdersViewModel> GetOrdersInProgress()
        {
            var statuses = new List<Enums.OrderStatus>() { Enums.OrderStatus.IN_PROGRESS };
            var orders = await _orderService.GetOrders(statuses);
            return orders;
        }
    }
}
