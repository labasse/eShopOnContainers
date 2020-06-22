using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Ordering.API.Models;
using Ordering.API.Services;

namespace Ordering.API
{
    public class OrderingServiceImpl : OrderingService.OrderingServiceBase
    {
        private readonly ILogger<OrderingServiceImpl> _logger;
        private readonly IOrderingRepository _repo;
        public OrderingServiceImpl(ILogger<OrderingServiceImpl> logger, IOrderingRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async override Task<ListOrdersResponse> ListOrders(ListOrdersRequest request, ServerCallContext context)
        {
            var response = new ListOrdersResponse();
            
            response.OrderIds.AddRange(await _repo.ListOrderIds(request.BuyerId));
            return response;
        }

        public async override Task<Order> GetOrder(GetOrderRequest request, ServerCallContext context) =>
            OrderAdapter.toGrpc(await _repo.GetOrder(request.OrderId));

        public async override Task<Order> UpdateOrder(Order request, ServerCallContext context) =>
            OrderAdapter.toGrpc(await _repo.UpdateOrder(OrderAdapter.fromGrpc(request)));        
    }
}
