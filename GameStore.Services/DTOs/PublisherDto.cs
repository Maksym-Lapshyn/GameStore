using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Services.DTOs
{
	public class PublisherDto : BaseEntity
	{
		public PublisherDto()
		{
			Games = new List<GameDto>();
		}

		public string CompanyName { get; set; }

		public string Description { get; set; }

		public string HomePage { get; set; }

		public IEnumerable<GameDto> Games { get; set; }
	}
}