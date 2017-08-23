using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.Web.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class OrderViewModel : BaseEntity
	{
		public OrderViewModel()
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}

		[Display(Name = "DateOrdered", ResourceType = typeof(GlobalResource))]
		public DateTime? DateOrdered { get; set; }

		[Display(Name = "DateShipped", ResourceType = typeof(GlobalResource))]
		public DateTime? DateShipped { get; set; }

		[Display(Name = "OrderStatus", ResourceType = typeof(GlobalResource))]
		public OrderStatus OrderStatus { get; set; }

		[Display(Name = "TotalPrice", ResourceType = typeof(GlobalResource))]
		public decimal TotalPrice { get; set; }

		public virtual UserViewModel User { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }
	}
}