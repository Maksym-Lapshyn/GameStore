namespace GameStore.Web.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public GameViewModel Game { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}