using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GameService : IGameService
	{
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
			var game = _mapper.Map<GameDto, Game>(gameDto);
			MapEmbeddedEntities(gameDto, game);
			game.ViewsCount = 0;
			game.DateAdded = DateTime.UtcNow;
			_gameRepository.Insert(game);
			_unitOfWork.Save();
		}

		public void Update(GameDto gameDto)
		{ //TODO Required: Get rid of commented code
			//var game = _gameRepository.GetAll(gameDto.Key);
			//_mapper.Map(gameDto, game);
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

		public GameDto GetSingleBy(string gameKey)
		{
			var game = _gameRepository.GetSingle(gameKey);
			game = ConvertToPoco(game);
			game.ViewsCount++;
			_gameRepository.Update(game);
			_unitOfWork.Save();
			var gameDto = _mapper.Map<Game, GameDto>(game);

			return gameDto;
		}

		public IEnumerable<GameDto> GetAll(GameFilterDto filterDto = null, int? skip = null, int? take = null)
		{
			IEnumerable<Game> games;

			if (filterDto != null)
			{
				var filter = _mapper.Map<GameFilterDto, GameFilter>(filterDto);
				games = _gameRepository.GetAll(filter, skip, take);
			}
			else
			{
				games = _gameRepository.GetAll(null, skip, take);
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

		public IEnumerable<GameDto> GetBy(string genreName)
		{
			var games = _gameRepository
				.GetAll().Where(game => game.Genres.Any(genre => genre.Name.ToLower() == genreName.ToLower()));
			var gameDtOs = _mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);

			return gameDtOs;
		}

		public IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames)
		{
			var allGames = _gameRepository.GetAll();
			var matchedGames = (from game in allGames from type in game.PlatformTypes where platformTypeNames.Contains(type.Type) select game);
			var gameDtOs = _mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(matchedGames);

			return gameDtOs;
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
			input.GenresInput.ForEach(n => result.Genres.Add(_genreRepository.GetSingle(n)));
			input.PlatformTypesInput.ForEach(p => result.PlatformTypes.Add(_platformTypeRepository.GetSingle(p)));
			result.Publisher = _publisherRepository.GetSingle(input.PublisherInput);

			return result;
		}
	}
}