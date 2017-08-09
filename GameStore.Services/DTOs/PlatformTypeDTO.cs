using GameStore.Common.Entities;
using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class PlatformTypeDto : BaseEntity
	{
		public PlatformTypeDto()
		{
			Games = new List<GameDto>();
		}

		public string Type { get; set; }

		public IEnumerable<GameDto> Games { get; set; }
	}
}