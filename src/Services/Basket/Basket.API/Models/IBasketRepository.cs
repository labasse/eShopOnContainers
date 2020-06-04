using System.Collections.Generic;

namespace Basket.API.Models
{
    public interface IBasketRepository
    {
        CustomerBasket GetBasketAsync(string customerId);
        
        CustomerBasket UpdateBasketAsync(CustomerBasket basket);
        bool DeleteBasketAsync(string id);
    }
}