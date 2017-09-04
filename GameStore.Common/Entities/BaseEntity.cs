using GameStore.Common.App_LocalResources;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Common.Entities
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonElement("_id")]
		public string NorthwindId { get; set; }

		[Display(Name = "IsDeleted", ResourceType = typeof(GlobalResource))]
		public bool IsDeleted { get; set; }
	}
}