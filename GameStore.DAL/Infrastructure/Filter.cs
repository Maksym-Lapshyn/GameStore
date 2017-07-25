using GameStore.Common.Enums;
using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Infrastructure
{
	public class Filter
	{
		public List<Genre> GenresData { get; set; }

		public List<int> GenresInput { get; set; }

		public List<PlatformType> PlatformTypesData { get; set; }

		public List<int> PlatformTypesInput { get; set; }

		public List<Publisher> PublishersData { get; set; }

		public List<int> PublishersInput { get; set; }

		public SortOptions SortOptions { get; set; }

		public decimal MinPrice { get; set; }

		public decimal MaxPrice { get; set; }

		public DateOptions DateOptions { get; set; }

		public string GameName { get; set; }
	}
}