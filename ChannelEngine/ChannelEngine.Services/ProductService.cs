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
        private readonly ChannelEngineService _channelEngineService;

        public ProductService(ChannelEngineService channelEngineService)
        {
            _channelEngineService = channelEngineService;
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

        public ProductEditStockViewModel CreateProductEditStockViewModel(string merchantProductNo)
        {
            return new ProductEditStockViewModel() { MerchantProductNo = merchantProductNo };
        }

        public async Task<bool> UpdateStock(ProductEditStockViewModel model)
        {
            // I usually do some extra validation here

            var isUpdated = await _channelEngineService.UpdateProductStock(model.MerchantProductNo, model.Stock);

            return isUpdated;
        }

        private async Task<OrdersViewModel> GetOrdersInProgress()
        {
            var statuses = new List<Enums.OrderStatus>() { Enums.OrderStatus.IN_PROGRESS };
            var orders = await _channelEngineService.GetOrders(statuses);
            return orders;
        }
    }
}
