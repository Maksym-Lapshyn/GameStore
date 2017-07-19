using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class AllCommentsViewModel
	{
		public AllCommentsViewModel() //TODO Required: Move to top
		{
			Comments = new List<CommentViewModel>();
		}

		public int GameId { get; set; }

		public string GameKey { get; set; }

		public List<CommentViewModel> Comments { get; set; }
	}
}