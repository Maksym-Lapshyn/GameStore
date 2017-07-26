using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DAL.Entities
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonElement("_id")]
		public string NorthwindId { get; set; }

		public bool IsDeleted { get; set; }
	}
}