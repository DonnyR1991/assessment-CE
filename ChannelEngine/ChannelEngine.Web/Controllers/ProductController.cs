using ChannelEngine.Common;
using ChannelEngine.Services;
using ChannelEngine.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IChannelEngineService _channelEngineService;

        public ProductController(IProductService productService, 
            IChannelEngineService channelEngineService)
        {
            _productService = productService;
            _channelEngineService = channelEngineService;
        }

        public async Task<IActionResult> Index()
        {
            var statuses = new List<Enums.OrderStatus>() { Enums.OrderStatus.IN_PROGRESS };
            var orders = await _channelEngineService.GetOrders(statuses);

            var model = _productService.GetTopSoldProducts(orders, 5);

            return View(model);
        }

        public IActionResult EditStock(string id)
        {
            var model = _productService.CreateProductEditStockViewModel(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStock(ProductEditStockViewModel model)
        {
            if (model.Stock != 25)
            {
                ModelState.AddModelError(nameof(model.Stock), "Invalid stock value, only 25 is allowed");
            }

            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateStock(model);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
