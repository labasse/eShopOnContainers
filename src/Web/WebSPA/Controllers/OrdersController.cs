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
        public ActionResult<IEnumerable<Order>> Get(string buyerId, [FromQuery] string pageToken = "1", [FromQuery] int? pageSize = 10)
        {
            var response = _serviceOrdering.ListOrders(
                new ListOrdersRequest { BuyerId = buyerId, PageToken = pageToken, PageSize = pageSize.Value },
                new CallOptions()
            );
            return response.Orders;
        }
    }
}
