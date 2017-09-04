using System.Collections.Generic;

namespace PaymentService.Entities
{
	public class User
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public List<Account> Accounts { get; set; }
	}
}