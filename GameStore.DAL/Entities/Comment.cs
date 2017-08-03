﻿using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
	public class Comment : BaseEntity
	{
		public string Name { get; set; }

		public string Body { get; set; }

		public string GameKey { get; set; }

		public int? ParentCommentId { get; set; }

		public virtual ICollection<Comment> ChildComments { get; set; }
	}
}