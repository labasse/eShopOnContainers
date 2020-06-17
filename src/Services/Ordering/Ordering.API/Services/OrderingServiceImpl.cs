using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Ordering.API
{
    public class OrderingServiceImpl : OrderingService.OrderingServiceBase
    {
        private readonly ILogger<OrderingServiceImpl> _logger;
        public OrderingServiceImpl(ILogger<OrderingServiceImpl> logger)
        {
            _logger = logger;
        }

        public override Task<ListOrdersResponse> ListOrders(ListOrdersRequest request, ServerCallContext context)
        {
            // TODO : use request to query a repository

            // TODO : prepare response
            var response = new ListOrdersResponse();
            response.NextPageToken = string.Empty;
            response.PageCount = 1;
            response.Orders.Add(new Order() { Number = "123456" });
            return Task.FromResult(response);
        }
    }
}
