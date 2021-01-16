using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.ViewModels.Order
{
    public class OrdersViewModel
    {
        public int Count { get; set; }
        public int Totalcount { get; set; }
        public int ItemsPerPage { get; set; }
        public int StatusCode { get; set; }
        public int? LogId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public IEnumerable<OrderViewModel> Content { get; set; }
    }
}
