namespace PaymentService.Entities
{
	public class Account
	{
		public User User { get; set; }

		public decimal Balance { get; set; }
	}
}