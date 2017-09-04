using GameStore.Common.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Common.Enums
{
	public enum SortOptions
	{
		[Display(Name = "SortByNone", ResourceType = typeof(GlobalResource))]
		None,

		[Display(Name = "SortyByMostViewed", ResourceType = typeof(GlobalResource))]
		MostViewed,

		[Display(Name = "SortByMostCommented", ResourceType = typeof(GlobalResource))]
		MostCommented,

		[Display(Name = "SortByPriceAscending", ResourceType = typeof(GlobalResource))]
		PriceAscending,

		[Display(Name = "SortByPriceDescending", ResourceType = typeof(GlobalResource))]
		PriceDescending,

		[Display(Name = "SortByDateAdded", ResourceType = typeof(GlobalResource))]
		DateAdded
	}
}