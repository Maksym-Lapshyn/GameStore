namespace GameStore.DAL.Entities
{
	public class OrderDetails : BaseEntity
	{
		public string GameKey { get; set; }

		public virtual Game Game { get; set; }

		public decimal Price { get; set; }

		public short Quantity { get; set; }

		public float Discount { get; set; }
	}
}