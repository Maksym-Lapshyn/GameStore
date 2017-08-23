using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameStore.Web.Models
{
	public class OrderViewModel : BaseEntity
	{
		public OrderViewModel()
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}

		[DisplayName("Order Id")]
		public int OrderId { get; set; }

		[DisplayName("Date ordered")]
		public DateTime? DateOrdered { get; set; }

		[DisplayName("Date shipped")]
		public DateTime? DateShipped { get; set; }

		[DisplayName("Order status")]
		public OrderStatus OrderStatus { get; set; }

		[DisplayName("Total price")]
		public decimal TotalPrice { get; set; }

		public virtual UserViewModel User { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }
	}
}