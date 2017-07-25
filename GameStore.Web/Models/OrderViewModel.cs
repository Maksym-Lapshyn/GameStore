using System;
using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class OrderViewModel
	{
		public OrderViewModel()
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}

		public string Id { get; set; }

		public string CustomerId { get; set; }

		public DateTime Date { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }
	}
}