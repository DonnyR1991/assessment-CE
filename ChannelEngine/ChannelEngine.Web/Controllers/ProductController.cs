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
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetTopSoldProducts(5);

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _productService.GetByMerchantProductNo(id);

            return View(model);
        }
    }
}
