using System.Collections.Generic;
using GameStore.Common.Enums;

namespace GameStore.Services.DTOs
{
	public class FilterDto
	{
		public FilterDto()
		{
			GenresData = new List<GenreDto>();
			GenresInput = new List<int>();
			PlatformTypesData = new List<PlatformTypeDto>();
			PlatformTypesInput = new List<int>();
			PublishersData = new List<PublisherDto>();
			PublishersInput = new List<int>();
		}

		public List<GenreDto> GenresData { get; set; }

		public List<int> GenresInput { get; set; }

		public List<PlatformTypeDto> PlatformTypesData { get; set; }

		public List<int> PlatformTypesInput { get; set; }

		public List<PublisherDto> PublishersData { get; set; }

		public List<int> PublishersInput { get; set; }

		public SortOptions SortOptions { get; set; }

		public decimal MinPrice { get; set; }

		public decimal MaxPrice { get; set; }

		public DateOptions DateOptions { get; set; }

		public string GameName { get; set; }
	}
}