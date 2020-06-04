using Newtonsoft.Json;
using StackExchange.Redis;

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

        public bool DeleteBasket(string id)
        {
            return _database.KeyDelete(id);
        }

        public CustomerBasket GetBasket(string customerId)
        {
            var data = _database.StringGet(customerId);

            return data.IsNullOrEmpty 
                ? null
                : JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public CustomerBasket UpdateBasket(CustomerBasket basket)
        {
            return _database.StringSet(basket.BuyerId, JsonConvert.SerializeObject(basket))
                ? GetBasket(basket.BuyerId)
                : null;
        }
    }
}
