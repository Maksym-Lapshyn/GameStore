using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Publisher : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		public string CompanyName { get; set; }

		[Column(TypeName = "NTEXT")]
		public string Description { get; set; }

		[Column(TypeName = "NTEXT")]
		public string HomePage { get; set; }

		public virtual ICollection<Game> Games { get; set; }
	}
}