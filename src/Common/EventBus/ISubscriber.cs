using System.Threading.Tasks;

namespace eShopOnContainers.Common.EventBus
{
    public interface ISubscriber<T> where T : IMessage
    {
        Task OnReceived(T message);
    }
}
