using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Database
{
    public class Basket
    {
        [Key]
        public int BuyerId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
