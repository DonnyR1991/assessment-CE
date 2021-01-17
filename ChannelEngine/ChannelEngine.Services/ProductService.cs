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
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetTopSoldProducts(OrdersViewModel orders, int amount);
        ProductEditStockViewModel CreateProductEditStockViewModel(string merchantProductNo);
        Task<bool> UpdateStock(ProductEditStockViewModel model);
    }

    public class ProductService : IProductService
    {
        private readonly IChannelEngineService _channelEngineService;

        public ProductService(IChannelEngineService channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }

        public IEnumerable<ProductViewModel> GetTopSoldProducts(OrdersViewModel orders, int amount)
        {
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
    }
}
