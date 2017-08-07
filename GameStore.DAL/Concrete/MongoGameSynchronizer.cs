using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete
{
	public class MongoGameSynchronizer : ISynchronizer<Game>
	{
		private readonly IMongoGameRepository _mongoRepository;

		public MongoGameSynchronizer(IMongoGameRepository mongoRepository)
		{
			_mongoRepository = mongoRepository;
		}

		public Game Synchronize(Game game)
		{
			if (!string.IsNullOrEmpty(game.NorthwindId) && !game.IsUpdated)
			{
				var gameFromMongo = _mongoRepository.GetSingle(game.Key);
				game.Name = gameFromMongo.Name;
				game.Price = gameFromMongo.Price;
				game.UnitsInStock = gameFromMongo.UnitsInStock;
				game.DatePublished = gameFromMongo.DatePublished;
				game.Description = gameFromMongo.Description;
				game.Discontinued = gameFromMongo.Discontinued;
			}

			return game;
		}
	}
}