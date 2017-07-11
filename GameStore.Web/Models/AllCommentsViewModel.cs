using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class AllCommentsViewModel
	{
		public int GameId { get; set; }

		public string GameKey { get; set; }

		public List<CommentViewModel> Comments { get; set; }

		public AllCommentsViewModel()
		{
			Comments = new List<CommentViewModel>();
		}
	}
}