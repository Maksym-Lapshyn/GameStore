using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GameStore.Common.Entities
{
	public class Comment : BaseEntity
	{
		public string Name { get; set; }

		public string Body { get; set; }

		public string GameKey { get; set; }

		public int? ParentCommentId { get; set; }

		[BsonIgnore]
		public virtual Comment ParentComment { get; set; }

		public virtual ICollection<Comment> ChildComments { get; set; }
	}
}