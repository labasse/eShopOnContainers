namespace eShopOnContainers.Common.EventBus.Messages
{
    public class OrderStateMsg : IMessage
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}