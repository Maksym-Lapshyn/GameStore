using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public virtual GameViewModel Game { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public int? OrderId { get; set; }
        public OrderViewModel Order { get; set; }
    }
}