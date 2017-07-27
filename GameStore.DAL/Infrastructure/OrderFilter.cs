using System;

namespace GameStore.DAL.Infrastructure
{
	public class OrderFilter
	{
		public DateTime From { get; set; }

		public DateTime To { get; set; }
	}
}