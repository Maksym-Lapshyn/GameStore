using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Services.Dtos
{
	public class CommentDto : BaseEntity
	{
		public CommentDto()
		{
			ChildComments = new List<CommentDto>();
		}

		public string Name { get; set; }

		public string Body { get; set; }

		public string GameKey { get; set; }

		public int? ParentCommentId { get; set; }

		public List<CommentDto> ChildComments { get; set; }
	}
}