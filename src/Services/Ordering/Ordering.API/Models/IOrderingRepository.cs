using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public interface IOrderingRepository
    {
        Task<IEnumerable<Order>> GetOrders(string orderId);

        Task<Order> CreateOrder(string OrderId);      
    }
}