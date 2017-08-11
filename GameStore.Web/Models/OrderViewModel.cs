using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using GameStore.Common.Enums;

namespace GameStore.Web.Models
{
	public class OrderViewModel : BaseEntity
	{
		public OrderViewModel()
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}

		public string NorthwindCustomerId { get; set; }

		public int OrderId { get; set; }

		public DateTime OrderedDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public User User { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }
	}
}