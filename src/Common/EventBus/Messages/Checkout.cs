using System;
using System.Collections.Generic;
using System.Text;

namespace eShopOnContainers.Common.EventBus.Messages
{
    public class Checkout : IMessage
    {
        public string BuyerId { get; set; }
        public ICollection<Item> Items { get; set; }

        public class Item
        {
            public int ProductId { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
}
