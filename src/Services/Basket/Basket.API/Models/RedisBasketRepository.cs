using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Basket.API.Models
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasket(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);

            return data.IsNullOrEmpty 
                ? null
                : JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            var res = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            return res
                ? await GetBasket(basket.BuyerId)
                : null;
        }
    }
}
