using GameStore.Common.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Common.Enums
{
	public enum OrderStatus
	{
		[Display(Name = "Active", ResourceType = typeof(GlobalResource))]
		Active,

		[Display(Name = "Paid", ResourceType = typeof(GlobalResource))]
		Paid,

		[Display(Name = "Shipped", ResourceType = typeof(GlobalResource))]
		Shipped
	}
}