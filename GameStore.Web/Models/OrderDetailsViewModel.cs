using GameStore.Common.Entities;

namespace GameStore.Web.Models
{
	public class OrderDetailsViewModel : BaseEntity
	{
		public string GameKey { get; set; }

		public GameViewModel Game { get; set; }

		public decimal Price { get; set; }

		public short Quantity { get; set; }

		public float Discount { get; set; }
	}
}