using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class Order
    {
        public string IdOrder { get; set; }
        public string BuyerId { get; set; }
        public object Data { get; set; }
    }
}
