using ChannelEngine.Common;
using ChannelEngine.Services;
using ChannelEngine.ViewModels.Product;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Console
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IChannelEngineService _channelEngineService;
        private readonly IProductService _productService;


        public App(ILogger<App> logger, IChannelEngineService channelEngineService, IProductService productService)
        {
            _logger = logger;
            _channelEngineService = channelEngineService;
            _productService = productService;
        }

        public async Task Run(string[] args)
        {
            _logger.LogInformation("Starting...");

            var statuses = new List<Enums.OrderStatus>() { Enums.OrderStatus.IN_PROGRESS };
            var orders = await _channelEngineService.GetOrders(statuses);

            var products = _productService.GetTopSoldProducts(orders, 5);

            System.Console.WriteLine($"Products found: {products.Count()}");

            foreach (var product in products)
            {
                System.Console.WriteLine($"{product.Description} {product.Gtin} {product.Quantity}");
            }

            var productEditStockViewModel = products.Select(p => new ProductEditStockViewModel()
            {
                MerchantProductNo = p.MerchantProductNo,
                Stock = 25
            }).FirstOrDefault();

            var result = await _productService.UpdateStock(productEditStockViewModel);

            System.Console.WriteLine($"Stock for {productEditStockViewModel.MerchantProductNo} updated: {result}");

            _logger.LogInformation("Finished!");

            await Task.CompletedTask;
        }
    }
}
