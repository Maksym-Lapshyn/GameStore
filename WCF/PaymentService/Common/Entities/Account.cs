using System;

namespace Common.Entities
{
	public class Account
	{
		public long CardNumber { get; set; }

		public int CvvCode { get; set; }

		public DateTime ExpirationDate { get; set; }

		public decimal Balance { get; set; }

		public User Owner { get; set; }
	}
}