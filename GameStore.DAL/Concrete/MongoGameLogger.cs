using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameStore.DAL.Concrete
{
	public class MongoGameLogger : ILogger<Game>
	{
		private readonly IMongoCollection<BsonDocument> _collection;

		public MongoGameLogger(IMongoDatabase database)
		{
			_collection = database.GetCollection<BsonDocument>("entity-changes");
		}

		public void LogChange(ILogContainer<Game> container)
		{
			var document = container.ToBsonDocument();

			_collection.InsertOne(document);
		}
	}
}