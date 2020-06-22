using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordering.API;
using Grpc.Core;

namespace WebSPA.Services
{
    public interface IOrderingService
    {
        Task<ListOrdersResponse> ListOrdersAsync(ListOrdersRequest request);
        Task<Order> GetOrderAsync(GetOrderRequest request);
        Task<Order> UpdateOrderAsync(Order request);
    }
}
