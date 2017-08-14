using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System;
using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class OrderViewModel : BaseEntity
	{
		public OrderViewModel()
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}

		public int OrderId { get; set; }

		public DateTime DateOrdered { get; set; }

		public DateTime? DateShipped { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public int? UserId { get; set; }

		public virtual UserViewModel User { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }
	}
}