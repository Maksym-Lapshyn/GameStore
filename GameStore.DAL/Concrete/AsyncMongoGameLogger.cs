using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace GameStore.DAL.Concrete
{
	public class AsyncMongoGameLogger : IAsyncLogger<Game>
	{
		private readonly IMongoCollection<BsonDocument> _collection;

		public AsyncMongoGameLogger(IMongoDatabase database)
		{
			_collection = database.GetCollection<BsonDocument>("entity-changes");
		}

		public async Task LogChangeAsync(ILogContainer<Game> container)
		{
			var document = container.ToBsonDocument();

			await _collection.InsertOneAsync(document);
		}
	}
}