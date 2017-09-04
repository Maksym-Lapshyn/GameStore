using GameStore.Common.Entities.Localization;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Common.Entities
{
	public class PlatformType : BaseEntity
	{
		[NotMapped]
		public string Type { get; set; }

		[BsonIgnore]
		public virtual ICollection<Game> Games { get; set; } = new List<Game>();

		[BsonIgnore]
		public virtual ICollection<PlatformTypeLocale> PlatformTypeLocales { get; set; } = new List<PlatformTypeLocale>();
	}
}