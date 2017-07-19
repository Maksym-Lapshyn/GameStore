using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class AllGamesViewModel
	{
		public FilterViewModel Filter { get; set; }

		public List<GameViewModel> Games { get; set; }
	
		public bool FilterIsChanged { get; set; }

		public int TotalItems { get; set; }

		public int CurrentPage { get; set; }

		public int PageSize { get; set; }

		public int TotalPages { get; set; }

		public AllGamesViewModel() //TODO Required: Move to top
		{
			Games = new List<GameViewModel>();
		}
	}
}