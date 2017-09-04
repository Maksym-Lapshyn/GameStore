using System;

namespace GameStore.DAL.Infrastructure
{
	public class OrderFilter
	{
		public DateTime BeginningDate { get; set; }

		public DateTime EndingDate { get; set; }
	}
}