using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class CommentDto
	{
		public CommentDto()
		{
			ChildComments = new List<CommentDto>();
		}

		public string Id { get; set; }

		public string Name { get; set; }

		public string Body { get; set; }

		public string GameId { get; set; }

		public GameDto Game { get; set; }

		public string ParentCommentId { get; set; }

		public CommentDto ParentComment { get; set; }

		public IEnumerable<CommentDto> ChildComments { get; set; }
	}
}