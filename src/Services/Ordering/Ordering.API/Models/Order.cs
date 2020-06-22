using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msg = eShopOnContainers.Common.EventBus.Messages;

namespace Ordering.API.Models
{
    public class Order
    {
        public string IdOrder { get; set; }
        public string BuyerId { get; set; }
        public ICollection<string> Lines { get; set; }
        public ICollection<OrderState> States { get; set; }
    }
}
