using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Linq;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Concrete
{
	public class GameCloner : ICloner<Game>
	{
		private readonly IEfGameRepository _gameRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICloner<Genre> _genreCloner;
		private readonly ICloner<Publisher> _publisherCloner;

		public GameCloner(IEfGameRepository gameRepository,
			IUnitOfWork unitOfWork,
			ICloner<Genre> genreCloner,
			ICloner<Publisher> publisherCloner)
		{
			_gameRepository = gameRepository;
			_unitOfWork = unitOfWork;
			_genreCloner = genreCloner;
			_publisherCloner = publisherCloner;
		}

		public Game Clone(Game game)
		{
			game.Publisher = _publisherCloner.Clone(game.Publisher);
			var genres = game.Genres.ToList();
			game.Genres.Clear();
			genres.ForEach(g => game.Genres.Add(_genreCloner.Clone(g)));
			_gameRepository.Insert(game);
			_unitOfWork.Save();

			return _gameRepository.GetSingle(game.Key);
		}
	}
}