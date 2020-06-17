using System.Threading.Tasks;
using WebSPA.Services.Basket;

namespace WebSPA.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket> SetAsync(CustomerBasket body);
        Task<CustomerBasket> GetAsync(string id);
        Task DeleteAsync(string id);        
    }
}
