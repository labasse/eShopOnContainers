using Newtonsoft.Json;
using Ordering.API.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShopOnContainers.Common.EventBus.Messages;

namespace Ordering.API.Models
{
    public class RedisOrderingRepository : IOrderingRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisOrderingRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<IEnumerable<string>> ListOrderIds(string buyerId)
            => (await GetAllOrderIds(buyerId)).Split(",");
            

        public async Task<Order> GetOrder(string orderId)
        {
            var data = await _database.StringGetAsync(orderId);

            return data.IsNullOrEmpty
                ? null
                : JsonConvert.DeserializeObject<Order>(data);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            order.IdOrder = await AddOrderId(order.BuyerId);
            return await UpdateOrder(order);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var res = await _database.StringSetAsync(order.IdOrder, JsonConvert.SerializeObject(order));
            return res
                ? await GetOrder(order.BuyerId)
                : null;
        }
        public async Task AddOrderStateAsync(OrderStateMsg message)
        {
            var order = await GetOrder(message.OrderId);

            order.States.Add(new OrderState
            {
                Name = message.Name,
                Data = message.Data
            });
            await UpdateOrder(order);
        }
        private async Task<string> GetAllOrderIds(string buyerId)
        {
            var all = await _database.StringGetAsync($"orders/{buyerId}");

            return all.ToString();
        }
        private async Task<string> AddOrderId(string buyerId)
        {
            var all = await GetAllOrderIds(buyerId);
            var orderId = new Guid().ToString();

            await _database.StringSetAsync(
                $"orders/{buyerId}",
                all.Length == 0 ? $"{orderId}" : $"{all},{orderId}"
            );
            return orderId;
        }
    }
}
