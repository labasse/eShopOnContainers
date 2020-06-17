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
        ListOrdersResponse ListOrders(ListOrdersRequest request, CallOptions options);
    }
}
