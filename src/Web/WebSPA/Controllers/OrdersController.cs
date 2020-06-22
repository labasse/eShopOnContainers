using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.API;
using WebSPA.Services;

namespace WebSPA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderingService _serviceOrdering;

        public OrdersController(IOrderingService serviceOrdering)
        {
            this._serviceOrdering = serviceOrdering;
        }

        [HttpGet("{buyerId}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string buyerId)
        {
            var response = await _serviceOrdering.ListOrdersAsync(
                new ListOrdersRequest { BuyerId = buyerId }
            );
            return response.OrderIds;
        }

        [HttpGet("{buyerId}/{orderId}")]
        public async Task<ActionResult<Order>> Get(string buyerId, string orderId) =>
            await _serviceOrdering.GetOrderAsync(new GetOrderRequest { OrderId = orderId });

        [HttpPost("{buyerId}/{orderId?}")]
        public async Task<ActionResult<Order>> Update([FromBody] Order order) =>
            await _serviceOrdering.UpdateOrderAsync(order);
    }
}
