using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System;
using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class OrderDto : BaseEntity
	{
		public OrderDto()
		{
			OrderDetails = new List<OrderDetailsDto>();
		}

		public string NorthwindCustomerId { get; set; }

		public int OrderId { get; set; }

		public DateTime OrderedDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public User User { get; set; }

		public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
	}
}