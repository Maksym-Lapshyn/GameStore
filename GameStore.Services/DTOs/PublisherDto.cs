using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class PublisherDto
	{
		public PublisherDto()
		{
			Games = new List<GameDto>();
		}

		public int Id { get; set; }

		public string CompanyName { get; set; }

		public string Description { get; set; }

		public string HomePage { get; set; }

		public IEnumerable<GameDto> Games { get; set; }
	}
}