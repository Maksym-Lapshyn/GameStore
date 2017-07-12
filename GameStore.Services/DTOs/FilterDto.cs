using GameStore.Services.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameStore.Services.DTOs
{
	public class FilterDto
	{
		public List<GenreDto> GenresData { get; set; }

		[DisplayName("Genre")]
		public List<int> GenresInput { get; set; }

		public List<PlatformTypeDto> PlatformTypesData { get; set; }

		[DisplayName("Platform")]
		public List<int> PlatformTypesInput { get; set; }

		public List<PublisherDto> PublishersData { get; set; }

		[DisplayName("Publisher")]
		public List<int> PublishersInput { get; set; }

		[DisplayName("Sort by")]
		public SortOptions SortOptions { get; set; }

		[DisplayName("Minimum price")]
		public decimal MinPrice { get; set; }

		[DisplayName("Maximum price")]
		public decimal MaxPrice { get; set; }

		[DisplayName("Published date")]
		public DateOptions DateOptions { get; set; }

		[DisplayName("Name")]
		public string GameName { get; set; }
	}
}