using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSPA.Services;

namespace Ordering.API
{
    public static partial class OrderingService
    {
        public partial class OrderingServiceClient : IOrderingService
        {
            private readonly CallOptions defOptions = new CallOptions();

            public async Task<ListOrdersResponse> ListOrdersAsync(ListOrdersRequest request) =>
                await ListOrdersAsync(request, defOptions);

            public async Task<Order> GetOrderAsync(GetOrderRequest request) =>
                await GetOrderAsync(request, defOptions);

            public async Task<Order> UpdateOrderAsync(Order request) =>
                await UpdateOrderAsync(request, defOptions);
        }
    }
}
