using ChannelEngine.Common;
using ChannelEngine.ViewModels.Options;
using ChannelEngine.ViewModels.Order;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ChannelEngine.Services
{
    public class OrderService
    {
        private static readonly HttpClient _client = new HttpClient();

        public OrderService(IOptions<ChannelEngineOptions> options)
        {
            _client.BaseAddress = new Uri(options.Value.ApiUri);
            _client.DefaultRequestHeaders.Add("X-CE-KEY", options.Value.ApiKey);
        }

        public async Task<OrdersViewModel> GetOrders(IEnumerable<Enums.OrderStatus> orderStatuses)
        {
            var query = HttpUtility.ParseQueryString("");

            foreach (var status in orderStatuses)
            {
                query["statuses"] = status.ToString();
            }

            var queryString = query.ToString();

            using (var response = await _client.GetAsync("orders?" + queryString))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<OrdersViewModel>(responseContent);
                }
            }

            return null;
        }
    }
}
