using System.Collections.Generic;

namespace Basket.API.Models
{
    public interface IBasketRepository
    {
        CustomerBasket GetBasket(string customerId);
        
        CustomerBasket UpdateBasket(CustomerBasket basket);
        bool DeleteBasket(string id);
    }
}