using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class AllGamesViewModel
	{
		public FilterViewModel Filter { get; set; }

		public List<GameViewModel> Games { get; set; }

		public PaginatorViewModel Paginator { get; set; }

		public AllGamesViewModel()
		{
			Games = new List<GameViewModel>();
		}
	}
}