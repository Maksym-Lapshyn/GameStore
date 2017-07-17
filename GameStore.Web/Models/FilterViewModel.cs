using GameStore.Web.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using GameStore.Web.Infrastructure.Attributes;

namespace GameStore.Web.Models
{
	public class FilterViewModel
	{
		[DisplayName("Platform type")]
		public List<CheckBoxListItem> Platforms { get; set; }

		[DisplayName("Genre")]
		public List<CheckBoxListItem> Genres { get; set; }

		[DisplayName("Publisher")]
		public List<CheckBoxListItem> Publisher { get; set; }

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

		public FilterViewModel()
		{
			Platforms = new List<CheckBoxListItem>();
			Genres = new List<CheckBoxListItem>();
			Publisher = new List<CheckBoxListItem>();
		}
	}
}