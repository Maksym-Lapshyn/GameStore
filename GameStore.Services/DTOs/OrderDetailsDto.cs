namespace GameStore.Services.DTOs
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public GameDto Game { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public int OrderId { get; set; }

        public OrderDto Order { get; set; }
    }
}
