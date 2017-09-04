using GameStore.Common.App_LocalResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class CompositeGamesViewModel
	{
		public CompositeGamesViewModel()
		{
			Games = new List<GameViewModel>();
		}

		public GameFilterViewModel FilterState { get; set; }

		public GameFilterViewModel Filter { get; set; }

		public List<GameViewModel> Games { get; set; }
	
		public bool FilterIsChanged { get; set; }

		public int TotalItems { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "ItemsPerPage", ResourceType = typeof(GlobalResource))]
		public int PageSize { get; set; }

		public int TotalPages { get; set; }

		public int StartPage { get; set; }

		public int EndPage { get; set; }
	}
}