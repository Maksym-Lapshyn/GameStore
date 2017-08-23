using GameStore.Common.Entities;

namespace GameStore.Services.Dtos
{
	public class OrderDetailsDto : BaseEntity
	{
		public GameDto Game { get; set; }

		public string GameKey { get; set; }

		public decimal Price { get; set; }

		public short Quantity { get; set; }

		public float Discount { get; set; }
	}
}