using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Game : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		public string Key { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		[BsonElement("UnitPrice")]
		[Column(TypeName = "MONEY")]
		public decimal Price { get; set; }

		[Column(TypeName="datetime2")]
		public DateTime DateAdded { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime DatePublished { get; set; }

		public int ViewsCount { get; set; }

		public short UnitsInStock { get; set; }

		public bool Discontinued { get; set; }

		public string PublisherId { get; set; }

		public virtual Publisher Publisher { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<Genre> Genres { get; set; }

		public virtual ICollection<PlatformType> PlatformTypes { get; set; }
	}
}
