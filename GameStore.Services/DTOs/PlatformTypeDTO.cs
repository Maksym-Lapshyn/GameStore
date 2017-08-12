using GameStore.Common.Entities;
using System.Collections.Generic;

namespace GameStore.Services.Dtos
{
	public class PlatformTypeDto : BaseEntity
	{
		public PlatformTypeDto()
		{
			Games = new List<GameDto>();
		}

		public string Type { get; set; }

		public List<GameDto> Games { get; set; }
	}
}