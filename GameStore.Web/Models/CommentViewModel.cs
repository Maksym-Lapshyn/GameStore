using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Body { get; set; }

		public string ParentCommentName { get; set; }

		public string GameKey { get; set; }

		public GameViewModel Game { get; set; }

		public int? ParentCommentId { get; set; }

		public CommentViewModel ParentComment { get; set; }

		public List<CommentViewModel> ChildComments { get; set; }
	}
}