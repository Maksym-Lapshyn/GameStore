using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Genre : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		[BsonElement("CategoryName")]
		public string Name { get; set; }

		public virtual ICollection<Genre> ChildGenres { get; set; }

		public virtual ICollection<Game> Games { get; set; }
	}
}
