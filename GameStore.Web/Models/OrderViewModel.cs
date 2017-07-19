using System;
using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class OrderViewModel
	{
		public OrderViewModel() //TODO Required: Move to top
		{
			OrderDetails = new List<OrderDetailsViewModel>();
		}

		public int Id { get; set; }

		public string CustomerId { get; set; }

		public DateTime Date { get; set; }

		public List<OrderDetailsViewModel> OrderDetails { get; set; }
	}
}