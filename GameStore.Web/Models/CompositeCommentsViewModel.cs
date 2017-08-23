using System.Collections.Generic;

namespace GameStore.Web.Models
{
	public class CompositeCommentsViewModel
	{
		public CompositeCommentsViewModel()
		{
			Comments = new List<CommentViewModel>();
		}

		public bool GameIsDeleted { get; set; }

		public List<CommentViewModel> Comments { get; set; }

		public CommentViewModel NewComment { get; set; }
	}
}