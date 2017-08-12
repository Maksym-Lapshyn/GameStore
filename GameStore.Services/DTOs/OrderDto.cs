using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System;
using System.Collections.Generic;

namespace GameStore.Services.Dtos
{
	public class OrderDto : BaseEntity
	{
		public OrderDto()
		{
			OrderDetails = new List<OrderDetailsDto>();
		}

        public string UserLogin { get; set; }

        public int OrderId { get; set; }

        public DateTime DateOrdered { get; set; }

        public DateTime? DateShipped { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public List<OrderDetailsDto> OrderDetails { get; set; }
	}
}