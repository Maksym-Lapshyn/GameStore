using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GameStore.Common.Entities
{
	public class PlatformType : BaseEntity
	{
		public string Type { get; set; }

		[BsonIgnore]
		public ICollection<Game> Games { get; set; }
	}
}