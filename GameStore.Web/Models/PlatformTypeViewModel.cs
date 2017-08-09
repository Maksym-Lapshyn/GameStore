using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Web.Models
{
	public class PlatformTypeViewModel : BaseEntity
	{
		public PlatformTypeViewModel()
		{
			Games = new List<GameViewModel>();
		}

		public string Type { get; set; }

		public List<GameViewModel> Games { get; set; }
	}
}