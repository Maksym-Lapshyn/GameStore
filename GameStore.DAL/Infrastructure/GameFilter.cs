using GameStore.Common.Enums;
using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Infrastructure
{
	public class GameFilter
	{
		public List<Genre> GenresData { get; set; }

		public List<string> GenresInput { get; set; }

		public List<PlatformType> PlatformTypesData { get; set; }

		public List<string> PlatformTypesInput { get; set; }

		public List<Publisher> PublishersData { get; set; }

		public List<string> PublishersInput { get; set; }

		public SortOptions SortOptions { get; set; }

		public decimal MinPrice { get; set; }

		public decimal MaxPrice { get; set; }

		public DateOptions DateOptions { get; set; }

		public string GameName { get; set; }
	}
}