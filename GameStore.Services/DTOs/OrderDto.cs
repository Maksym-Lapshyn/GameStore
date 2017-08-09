using System;
using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Services.DTOs
{
	public class OrderDto : BaseEntity
	{
		public OrderDto()
		{
			OrderDetails = new List<OrderDetailsDto>();
		}

		public string CustomerId { get; set; }

		public DateTime Date { get; set; }

		public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
	}
}