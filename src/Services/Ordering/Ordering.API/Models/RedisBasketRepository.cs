using Newtonsoft.Json;
using Ordering.API.Models;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Models
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

        public Task<Ordering.API.Models.Order> CreateOrder(string OrderId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        // public async Task<CustomerBasket> GetBasket(string customerId)
        // {
        //     var data = await _database.StringGetAsync(customerId);
        // 
        //     return data.IsNullOrEmpty 
        //         ? null
        //         : JsonConvert.DeserializeObject<CustomerBasket>(data);
        // }

        public Task<IEnumerable<Ordering.API.Models.Order>> GetOrders(string orderId)
        {
            throw new System.NotImplementedException();
        }

        // public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        // {
        //     var res = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
        //     return res
        //         ? await GetBasket(basket.BuyerId)
        //         : null;
        // }
    }
}
