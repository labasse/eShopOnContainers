using eShopOnContainers.Common.EventBus;
using eShopOnContainers.Common.EventBus.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class OrderStateUpdater : ISubscriber<OrderStateMsg>
    {
        private IOrderingRepository _repo;

        public OrderStateUpdater(IEventBus eventBus, IOrderingRepository repo)
        {
            _repo = repo;
            eventBus.Subscribe(this);
        }
        public async Task OnReceived(OrderStateMsg message) => 
            await _repo.AddOrderStateAsync(message);  
    }
}
