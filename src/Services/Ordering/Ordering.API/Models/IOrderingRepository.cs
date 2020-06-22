using eShopOnContainers.Common.EventBus.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public interface IOrderingRepository
    {
        Task<IEnumerable<string>> ListOrderIds(string buyerId);

        Task<Order> GetOrder(string orderId);

        Task<Order> UpdateOrder(Order order);
        Task AddOrderStateAsync(OrderStateMsg message);
    }
}