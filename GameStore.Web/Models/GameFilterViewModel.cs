using GameStore.Web.Infrastructure.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using GameStore.Common.Enums;

namespace GameStore.Web.Models
{
	public class GameFilterViewModel
	{
		public GameFilterViewModel()
		{
			GenresData = new List<GenreViewModel>();
			GenresInput = new List<string>();
			PlatformTypesData = new List<PlatformTypeViewModel>();
			PlatformTypesInput = new List<string>();
			PublishersData = new List<PublisherViewModel>();
			PublishersInput = new List<string>();
		}

		[DisplayName("Genres")]
		public List<GenreViewModel> GenresData { get; set; }

		public List<string> GenresInput { get; set; }

		[DisplayName("Platform types")]
		public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

		public List<string> PlatformTypesInput { get; set; }

		[DisplayName("Publishers")]
		public List<PublisherViewModel> PublishersData { get; set; }

		public List<string> PublishersInput { get; set; }

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
	}
}