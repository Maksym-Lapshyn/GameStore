namespace GameStore.Web.Models
{
	public class OrderDetailsViewModel
	{
		public string Id { get; set; }

		public string GameId { get; set; }

		public GameViewModel Game { get; set; }

		public decimal Price { get; set; }

		public short Quantity { get; set; }

		public float Discount { get; set; }
	}
}