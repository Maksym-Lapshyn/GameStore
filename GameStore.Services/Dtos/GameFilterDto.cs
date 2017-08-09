using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class GameFilterDto : BaseEntity
	{
		public GameFilterDto()
		{
			GenresData = new List<GenreDto>();
			GenresInput = new List<string>();
			PlatformTypesData = new List<PlatformTypeDto>();
			PlatformTypesInput = new List<string>();
			PublishersData = new List<PublisherDto>();
			PublishersInput = new List<string>();
		}

		public List<GenreDto> GenresData { get; set; }

		public List<string> GenresInput { get; set; }

		public List<PlatformTypeDto> PlatformTypesData { get; set; }

		public List<string> PlatformTypesInput { get; set; }

		public List<PublisherDto> PublishersData { get; set; }

		public List<string> PublishersInput { get; set; }

		public SortOptions SortOptions { get; set; }

		public decimal MinPrice { get; set; }

		public decimal MaxPrice { get; set; }

		public DateOptions DateOptions { get; set; }

		public string GameName { get; set; }
	}
}