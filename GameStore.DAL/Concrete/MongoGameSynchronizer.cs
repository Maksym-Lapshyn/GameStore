using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;

namespace GameStore.DAL.Concrete
{
	public class MongoGameSynchronizer : ISynchronizer<Game>
	{
		private readonly IMongoGameRepository _mongoRepository;
		private readonly IEfGameRepository _efRepository;

		public MongoGameSynchronizer(IMongoGameRepository mongoRepository, IEfGameRepository efRepository)
		{
			_mongoRepository = mongoRepository;
			_efRepository = efRepository;
		}

		public Game Synchronize(Game game)
		{
			if (!string.IsNullOrEmpty(game.NorthwindId) && !game.IsUpdated && game.Id != default(int))
			{
				var gameFromMongo = _mongoRepository.GetSingle(g => g.Key == game.Key);
				game.Name = gameFromMongo.Name;
				game.Price = gameFromMongo.Price;
				game.UnitsInStock = gameFromMongo.UnitsInStock;
				game.DatePublished = gameFromMongo.DatePublished;
				game.Description = gameFromMongo.Description;
				game.Discontinued = gameFromMongo.Discontinued;

				_efRepository.Update(game);
			}

			return game;
		}
	}
}