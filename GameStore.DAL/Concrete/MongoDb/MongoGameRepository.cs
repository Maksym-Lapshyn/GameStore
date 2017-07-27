using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System;
using System.Linq;

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

		public IQueryable<Game> Get()
		{
			var games = _gameCollection.AsQueryable().ToList();

			for (var i = 0; i < games.Count; i++)
			{
				games[i] = GetEmbeddedDocuments(games[i]);
				games[i].DateAdded = new DateTime(2017, 06, 07);
				games[i].DatePublished = DateTime.MinValue;
			}

			return games.AsQueryable();
		}

		public Game Get(string gameKey)
		{
			var game =  _gameCollection.Find(g => g.Key == gameKey).Single();
			game = GetEmbeddedDocuments(game);

			return game;
		}

		private Game GetEmbeddedDocuments(Game game)
		{
			game.Publisher = _publisherCollection.AsQueryable().First(p => p.SupplierId == game.SupplierId);
			game.Genres = _genreCollection.AsQueryable().Where(g => g.CategoryId == game.CategoryId).ToList();

			return game;
		}
	}
}