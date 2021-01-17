using ChannelEngine.Services;
using ChannelEngine.ViewModels.Order;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.Test
{
    [TestClass]
    public class ProductTest
    {
        private IChannelEngineService _channelEngineService;

        [TestInitialize]
        public void Setup()
        {
            var orders = new OrdersViewModel()
            {
                Content = new List<OrderViewModel>()
                {
                    new OrderViewModel()
                    {
                        Lines = new List<OrderLineViewModel>()
                        {
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "111",
                                Description = "Test111",
                                Gtin = "11111",
                                Quantity = 1
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "222",
                                Description = "Test222",
                                Gtin = "22222",
                                Quantity = 1
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "333",
                                Description = "Test333",
                                Gtin = "33333",
                                Quantity = 1
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "444",
                                Description = "Test444",
                                Gtin = "44444",
                                Quantity = 1
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "555",
                                Description = "Test555",
                                Gtin = "55555",
                                Quantity = 1
                            }
                        }
                    },
                    new OrderViewModel()
                    {
                        Lines = new List<OrderLineViewModel>()
                        {
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "111",
                                Description = "Test111",
                                Gtin = "11111",
                                Quantity = 1
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "222",
                                Description = "Test222",
                                Gtin = "22222",
                                Quantity = 2
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "333",
                                Description = "Test333",
                                Gtin = "33333",
                                Quantity = 3
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "444",
                                Description = "Test444",
                                Gtin = "44444",
                                Quantity = 4
                            },
                            new OrderLineViewModel()
                            {
                                MerchantProductNo = "555",
                                Description = "Test555",
                                Gtin = "55555",
                                Quantity = 5
                            }
                        }
                    }
                }
            };

            var channelEngineServiceMock = new Mock<IChannelEngineService>();
            channelEngineServiceMock.Setup(c => c.GetOrders(null)).ReturnsAsync(orders);
            _channelEngineService = channelEngineServiceMock.Object;
        }

        [TestMethod]
        public async Task Product_GetTopSold()
        {
            var productService = new ProductService(_channelEngineService);

            var orders = await _channelEngineService.GetOrders(null);
            var products = productService.GetTopSoldProducts(orders, 5);

            var mostSoldProduct = products.FirstOrDefault();

            Assert.AreEqual("555", mostSoldProduct.MerchantProductNo);
        }
    }
}
