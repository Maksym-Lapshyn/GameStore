using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;

namespace GameStore.DAL.Concrete
{
	public class MongoGameLogger : ILogger<Game>
	{
		private readonly IMongoCollection<GameLogContainer> _collection;

		public MongoGameLogger(IMongoDatabase database)
		{
			_collection = database.GetCollection<GameLogContainer>("entityChanges");
		}

		public void LogChange(ILogContainer<Game> container)
		{
			var document = (GameLogContainer) container;
			_collection.InsertOne(document);
		}
	}
}