using System;
using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class OrderDto
	{
		public OrderDto()
		{
			OrderDetails = new List<OrderDetailsDto>();
		}

		public string Id { get; set; }

		public bool IsDeleted { get; set; }

		public string CustomerId { get; set; }

		public DateTime Date { get; set; }

		public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
	}
}
