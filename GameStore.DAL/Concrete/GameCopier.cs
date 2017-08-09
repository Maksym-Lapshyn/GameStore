using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Linq;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Concrete
{
	public class GameCopier : ICopier<Game>
	{
		private readonly IEfGameRepository _gameRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICopier<Genre> _genreCopier;
		private readonly ICopier<Publisher> _publisherCopier;

		public GameCopier(IEfGameRepository gameRepository,
			IUnitOfWork unitOfWork,
			ICopier<Genre> genreCopier,
			ICopier<Publisher> publisherCopier)
		{
			_gameRepository = gameRepository;
			_unitOfWork = unitOfWork;
			_genreCopier = genreCopier;
			_publisherCopier = publisherCopier;
		}

		public Game Copy(Game game)
		{
			game.Publisher = _publisherCopier.Copy(game.Publisher);
			var genres = game.Genres.ToList();
			game.Genres.Clear();
			genres.ForEach(g => game.Genres.Add(_genreCopier.Copy(g)));
			_gameRepository.Insert(game);
			_unitOfWork.Save();

			return _gameRepository.GetSingle(game.Key);
		}
	}
}