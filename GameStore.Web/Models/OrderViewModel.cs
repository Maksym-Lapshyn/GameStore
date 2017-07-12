using System;
using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class OrderViewModel
	{
		public int Id { get; set; }

		public string CustomerId { get; set; }

		public DateTime Date { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }

		public OrderViewModel()
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}
	}
}