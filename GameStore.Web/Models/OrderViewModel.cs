using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<OrderDetailsViewModel> OrderDetails { get; set; }

        public OrderViewModel()
        {
            OrderDetails = new List<OrderDetailsViewModel>();
        }
    }
}