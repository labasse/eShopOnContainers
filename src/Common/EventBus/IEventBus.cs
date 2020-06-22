using System;

namespace eShopOnContainers.Common.EventBus
{
    public interface IEventBus : IDisposable
    {
        void Publish<T>(T publishedEvent) where T : IMessage;
        void Subscribe<T>(ISubscriber<T> subscriber) where T : IMessage;
    }
}
