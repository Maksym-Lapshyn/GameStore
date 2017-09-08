using GameStore.Common.Entities;
using GameStore.DAL.Abstract.MongoDb;
using MongoDB.Driver;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class AsyncMongoGameRepository : IAsyncMongoGameRepository
	{
		private readonly IMongoCollection<Game> _gameCollection;
		private readonly IMongoCollection<Publisher> _publisherCollection;
		private readonly IMongoCollection<Genre> _genreCollection;

		public AsyncMongoGameRepository(IMongoDatabase database)
		{
			_gameCollection = database.GetCollection<Game>("products");
			_publisherCollection = database.GetCollection<Publisher>("suppliers");
			_genreCollection = database.GetCollection<Genre>("categories");
		}

		public async Task<Game> GetSingleOrDefaultAsync(Expression<Func<Game, bool>> predicate)
		{
			var task = await IAsyncCursorSourceExtensions.ToListAsync(_gameCollection.AsQueryable());
			var game = task.Single(predicate.Compile());
			game = await GetEmbeddedDocumentsAsync(game);
			game = SetDefaultDates(game);

			return game;
		}

		private async Task<Game> GetEmbeddedDocumentsAsync(Game game)
		{
			var publisherTask = IAsyncCursorSourceExtensions.ToListAsync(_publisherCollection.AsQueryable());

			var genresTask = _genreCollection.AsQueryable().Where(g => g.CategoryId == game.CategoryId).ToListAsync();

			await Task.WhenAll(publisherTask, genresTask);

			game.Publisher = publisherTask.Result.First();
			game.Genres = genresTask.Result;

			return game;
		}

		private Game SetDefaultDates(Game game)
		{
			game.DateAdded = new DateTime(2017, 06, 07);
			game.DatePublished = DateTime.MinValue;

			return game;
		}
	}
}