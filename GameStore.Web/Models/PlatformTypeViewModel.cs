using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class PlatformTypeViewModel
	{
		public PlatformTypeViewModel()
		{
			Games = new List<GameViewModel>();
		}

		public int Id { get; set; }

		public string Type { get; set; }

		public List<GameViewModel> Games { get; set; }
	}
}