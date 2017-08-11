using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Common.Entities
{
	[BsonIgnoreExtraElements]
	public class Genre : BaseEntity
	{
		[BsonElement("CategoryName")]
		public string Name { get; set; }

		[BsonElement("CategoryID")]
		[NotMapped]
		public int CategoryId { get; set; }

		public virtual Genre ParentGenre { get; set; }

		[BsonIgnore]
		public virtual ICollection<Game> Games { get; set; }
	}
}