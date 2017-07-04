using System;
using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        public List<OrderDetailsDto> OrderDetails { get; set; }

        public OrderDto()
        {
            OrderDetails = new List<OrderDetailsDto>();
        }
    }
}
