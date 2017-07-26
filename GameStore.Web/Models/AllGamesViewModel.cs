using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class AllGamesViewModel
	{
		public AllGamesViewModel()
		{
			Games = new List<GameViewModel>();
		}

		public GameFilterViewModel FilterState { get; set; }

		public GameFilterViewModel Filter { get; set; }

		public List<GameViewModel> Games { get; set; }
	
		public bool FilterIsChanged { get; set; }

		public int TotalItems { get; set; }

		public int CurrentPage { get; set; }

		public int PageSize { get; set; }

		public int TotalPages { get; set; }

		public int StartPage { get; set; }

		public int EndPage { get; set; }
	}
}