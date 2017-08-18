using GameStore.Common.Entities;
using GameStore.DAL.Abstract.MongoDb;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoGameRepository : IMongoGameRepository
	{
		private readonly IMongoCollection<Game> _gameCollection;
		private readonly IMongoCollection<Publisher> _publisherCollection;
		private readonly IMongoCollection<Genre> _genreCollection;

		public MongoGameRepository(IMongoDatabase database)
		{
			_gameCollection = database.GetCollection<Game>("products");
			_publisherCollection = database.GetCollection<Publisher>("suppliers");
			_genreCollection = database.GetCollection<Genre>("categories");
		}

		public IQueryable<Game> GetAll(Expression<Func<Game, bool>> predicate = null)
		{
			var games = predicate != null 
				? _gameCollection.AsQueryable().Where(predicate.Compile()).ToList()
				: _gameCollection.AsQueryable().ToList();

			for (var i = 0; i < games.Count; i++)
			{
				games[i] = GetEmbeddedDocuments(games[i]);
				games[i].DateAdded = new DateTime(2017, 06, 07);
				games[i].DatePublished = DateTime.MinValue;
			}

			return games.AsQueryable();
		}

		public Game GetSingle(Expression<Func<Game, bool>> predicate)
		{
			var game =  _gameCollection.Find(predicate).Single();
			game = GetEmbeddedDocuments(game);

			return game;
		}

		public bool Contains(Expression<Func<Game, bool>> predicate)
		{
			return _gameCollection.AsQueryable().Any(predicate);
		}

		private Game GetEmbeddedDocuments(Game game)
		{
			game.Publisher = _publisherCollection.AsQueryable().First(p => p.SupplierId == game.SupplierId);
			game.Genres = _genreCollection.AsQueryable().Where(g => g.CategoryId == game.CategoryId).ToList();

			return game;
		}
	}
}