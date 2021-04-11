using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Database
{
    public class BasketItem
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Basket")]
        public int BuyerId { get; set; }
        public Basket Basket { get; set; }
    }
}
