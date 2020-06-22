using eShopOnContainers.Common.EventBus;
using eShopOnContainers.Common.EventBus.Messages;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class StockUpdater : ISubscriber<Checkout>
    {
        private ICatalogRepo _repo;

        public StockUpdater(IEventBus eventBus, ICatalogRepo repo) 
        {
            _repo = repo;
            eventBus.Subscribe(this);
        }

        public async Task OnReceived(Checkout message)
        {
            foreach(var item in message.Items)
            {
                await _repo.RemoveFromStock(item.ProductId, item.Quantity);
            }
        }
    }
}
