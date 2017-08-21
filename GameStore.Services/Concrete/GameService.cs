using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GameService : IGameService
	{
		private const string DefaultGenreName = "Other";
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGameRepository _gameRepository;
		private readonly IPublisherRepository _publisherRepository;
		private readonly IGenreRepository _genreRepository;
		private readonly IPlatformTypeRepository _platformTypeRepository;

		public GameService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IGameRepository gameRepository,
			IPublisherRepository publisherRepository,
			IGenreRepository genreRepository,
			IPlatformTypeRepository platformTypeRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_gameRepository = gameRepository;
			_publisherRepository = publisherRepository;
			_platformTypeRepository = platformTypeRepository;
			_genreRepository = genreRepository;
		}

		public void Create(GameDto gameDto)
		{
			AddDefaultGenreInput(gameDto);
			var game = _mapper.Map<GameDto, Game>(gameDto);
			MapEmbeddedEntities(gameDto, game);
			game.ViewsCount = 0;
			game.DateAdded = DateTime.UtcNow;
			_gameRepository.Insert(game);
			_unitOfWork.Save();
		}

		public void Update(GameDto gameDto)
		{
			AddDefaultGenreInput(gameDto);
			var game = _mapper.Map<GameDto, Game>(gameDto);
			game.IsUpdated = true;
			game = MapEmbeddedEntities(gameDto, game);
			_gameRepository.Update(game);
			_unitOfWork.Save();
		}

		public void Delete(string gameKey)
		{
			_gameRepository.Delete(gameKey);
			_unitOfWork.Save();
		}

		public GameDto GetSingle(string gameKey)
		{
			var game = _gameRepository.GetSingle(g => g.Key == gameKey);
			game = ConvertToPoco(game);
			game.ViewsCount++;
			_gameRepository.Update(game);
			_unitOfWork.Save();
			var gameDto = _mapper.Map<Game, GameDto>(game);

			return gameDto;
		}

		public IEnumerable<GameDto> GetAll(GameFilterDto filterDto = null, int? itemsToSkip = null, int? itemsToTake = null, bool allowDeleted = false)
		{
			IEnumerable<Game> games;

			if (filterDto != null)
			{
				var filter = _mapper.Map<GameFilterDto, GameFilter>(filterDto);
				games = allowDeleted
					? _gameRepository.GetAll(filter, itemsToSkip, itemsToTake)
					: _gameRepository.GetAll(filter, itemsToSkip, itemsToTake, g => g.IsDeleted == false);
			}
			else
			{
				games = allowDeleted
					? _gameRepository.GetAll(null, itemsToSkip, itemsToTake)
					: _gameRepository.GetAll(null, itemsToSkip, itemsToTake, g => g.IsDeleted == false);
			}

			var gameDtos = _mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);

			return gameDtos;
		}

		public int GetCount(GameFilterDto gameFilter = null)
		{
			if (gameFilter != null)
			{
				var filter = _mapper.Map<GameFilterDto, GameFilter>(gameFilter);

				return _gameRepository.GetAll(filter).Count();
			}

			return _gameRepository.GetAll().Count();
		}

		public bool Contains(string gameKey)
		{
			return _gameRepository.Contains(g => g.Key == gameKey);
		}

		private Game ConvertToPoco(Game game)
		{
			var gameDto = _mapper.Map<Game, GameDto>(game);
			var result = _mapper.Map<GameDto, Game>(gameDto);
			result = MapEmbeddedEntities(gameDto, result);

			return result;
		}

		private Game MapEmbeddedEntities(GameDto input, Game result)
		{
			input.GenresInput.ForEach(n => result.Genres.Add(_genreRepository.GetSingle(g => g.Name == n)));
			input.PlatformTypesInput.ForEach(t => result.PlatformTypes.Add(_platformTypeRepository.GetSingle(p => p.Type == t)));
			result.Publisher = _publisherRepository.GetSingle(p => p.CompanyName == input.PublisherInput);

			return result;
		}

		private void AddDefaultGenreInput(GameDto game)
		{
			if (game.GenresInput.Count == 0)
			{
				game.GenresInput.Add(DefaultGenreName);
			}
		}
	}
}