using GameStore.Web.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using GameStore.Web.Infrastructure.Attributes;

namespace GameStore.Web.Models
{
	public class FilterViewModel
	{
		[DisplayName("Genres")]
		public List<GenreViewModel> GenresData { get; set; }

		public List<int> GenresInput { get; set; }

		[DisplayName("Platform types")]
		public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

		public List<int> PlatformTypesInput { get; set; }

		[DisplayName("Publishers")]
		public List<PublisherViewModel> PublishersData { get; set; }

		public List<int> PublishersInput { get; set; }

		[DisplayName("Sort by")]
		public SortOptions SortOptions { get; set; }

		[DisplayName("Minimum price")]
		public decimal MinPrice { get; set; }

		[DisplayName("Maximum price")]
		public decimal MaxPrice { get; set; }

		[DisplayName("Published date")]
		public DateOptions DateOptions { get; set; }

		[RequiredLengthIfNotNull(3)]
		[DisplayName("Name")]
		public string GameName { get; set; }

		public FilterViewModel() //TODO Required: Move to top
		{
			GenresData = new List<GenreViewModel>();
			GenresInput = new List<int>();
			PlatformTypesData = new List<PlatformTypeViewModel>();
			PlatformTypesInput = new List<int>();
			PublishersData = new List<PublisherViewModel>();
			PublishersInput = new List<int>();
		}
	}
}