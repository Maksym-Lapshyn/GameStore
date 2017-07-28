using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class AllOrdersViewModel
	{
		public AllOrdersViewModel()
		{
			Orders = new List<OrderViewModel>();
		}

		public OrderFilterViewModel Filter { get; set; }

		public List<OrderViewModel> Orders { get; set; }
	}
}