using System.Threading.Tasks;

namespace WebSPA.Services.Basket
{
    public partial class BasketAPIClient : IBasketService
    {
        public async Task DeleteAsync(string id) =>
            await Baskets2Async(id);

        public async Task<CustomerBasket> GetAsync(string id) =>
            await BasketsAsync(id);

        public async Task<CustomerBasket> SetAsync(CustomerBasket body) =>
            await Baskets3Async(body);        
    }
}
