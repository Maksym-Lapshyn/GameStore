using System;

namespace GameStore.Services.Dtos
{
	public class OrderFilterDto
	{
		public DateTime BeginningDate { get; set; }

		public DateTime EndingDate { get; set; }
	}
}