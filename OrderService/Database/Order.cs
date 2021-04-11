using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Database
{
    public class Order
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
