using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Models
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasket(string customerId);

        Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
        Task<bool> DeleteBasket(string id);
    }
}