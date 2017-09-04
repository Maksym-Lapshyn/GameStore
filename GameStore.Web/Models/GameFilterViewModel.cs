using GameStore.Common.App_LocalResources;
using GameStore.Common.Enums;
using GameStore.Web.Infrastructure.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

		public List<GenreViewModel> GenresData { get; set; }

		[Display(Name = "Genres", ResourceType = typeof(GlobalResource))]
		public List<string> GenresInput { get; set; }

		public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

		[Display(Name = "PlatformTypes", ResourceType = typeof(GlobalResource))]
		public List<string> PlatformTypesInput { get; set; }

		public List<PublisherViewModel> PublishersData { get; set; }

		[Display(Name = "Publishers", ResourceType = typeof(GlobalResource))]
		public List<string> PublishersInput { get; set; }

		[Display(Name = "ToSortBy", ResourceType = typeof(GlobalResource))]
		public SortOptions SortOptions { get; set; }

		[Display(Name = "MinimumPrice", ResourceType = typeof(GlobalResource))]
		public decimal MinPrice { get; set; }

		[Display(Name = "MaximumPrice", ResourceType = typeof(GlobalResource))]
		public decimal MaxPrice { get; set; }

		[Display(Name = "DatePublished", ResourceType = typeof(GlobalResource))]
		public DateOptions DateOptions { get; set; }

		[RequiredLengthIfNotNull(3, ErrorMessageResourceName = "GameNameShouldBeLongerThanThreeCharacters", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "GameName", ResourceType = typeof(GlobalResource))]
		public string GameName { get; set; }
	}
}