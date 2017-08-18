using System.ComponentModel;

namespace GameStore.Common.Enums
{
	public enum SortOptions
	{
		None,
		[Description("Most viewed")]
		MostViewed,
		[Description("Most commented")]
		MostCommented,
		[Description("Price ascending")]
		PriceAscending,
		[Description("Price descending")]
		PriceDescending,
		DateAdded
	}
}