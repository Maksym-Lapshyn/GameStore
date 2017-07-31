using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
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
		private readonly IEfGameRepository _gameRepository;
		private readonly IEfPublisherRepository _publisherRepository;
		private readonly IEfGenreRepository _genreRepository;
		private readonly IEfPlatformTypeRepository _platformTypeRepository;

		public GameService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IEfGameRepository gameRepository,
			IEfPublisherRepository publisherRepository,
			IEfGenreRepository genreRepository,
			IEfPlatformTypeRepository platformTypeRepository)
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
			MapDto(gameDto, game);
			game.ViewsCount = 0;
			game.DateAdded = DateTime.UtcNow;
			_gameRepository.Insert(game);
			_unitOfWork.Save();
		}

		public void Edit(GameDto gameDto)
		{
			var game = _gameRepository.Get().First(g => g.Id == gameDto.Id);
			game = _mapper.Map(gameDto, game);
			MapDto(gameDto, game);
			_gameRepository.Update(game);
			_unitOfWork.Save();
		}

		public void SaveView(string gameKey)
		{
			var game = _gameRepository.Get(gameKey);
			MapEntity(game);
			game.ViewsCount++;
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
			var game = _gameRepository.Get().First(g => g.Key.ToLower() == gameKey.ToLower());
			var gameDto = _mapper.Map<Game, GameDto>(game);

			return gameDto;
		}

		public IEnumerable<GameDto> GetAll(GameFilterDto filterDto = null, int? skip = null, int? take = null)
		{
			var games = _gameRepository.Get();

			if (filterDto != null)
			{
				var filter = _mapper.Map<GameFilterDto, GameFilter>(filterDto);
				games = _gameRepository.Get(filter);
			}

			if (skip != null && take != null)
			{
				games = games.Skip(skip.Value).Take(take.Value);
			}

			var gameDtos = _mapper.Map<IQueryable<Game>, IEnumerable<GameDto>>(games);

			return gameDtos;
		}

		public int GetCount(GameFilterDto gameFilter = null)
		{
			if (gameFilter != null)
			{
				var filter = _mapper.Map<GameFilterDto, GameFilter>(gameFilter);

				return _gameRepository.Get(filter).Count();
			}

			return _gameRepository.Get().Count();
		}

		public IEnumerable<GameDto> GetBy(string genreName)
		{
			var games = _gameRepository
				.Get().Where(game => game.Genres.Any(genre => genre.Name.ToLower() == genreName.ToLower()));
			var gameDtOs = _mapper.Map<IQueryable<Game>, IEnumerable<GameDto>>(games);

			return gameDtOs;
		}

		public IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames)
		{
			var allGames = _gameRepository.Get();
			var matchedGames = (from game in allGames from type in game.PlatformTypes where platformTypeNames.Contains(type.Type) select game);
			var gameDtOs = _mapper.Map<IQueryable<Game>, IEnumerable<GameDto>>(matchedGames);

			return gameDtOs;
		}

		private void MapDto(GameDto input, Game result)
		{
			result.Publisher = _publisherRepository.Get(input.PublisherInput);
			result.PlatformTypes = input.PlatformTypesInput.Select(type => _platformTypeRepository.Get(type)).ToList();
			result.Genres = input.GenresInput.Select(name => _genreRepository.Get(name)).ToList();
		}

		private void MapEntity(Game result)
		{
			if (result.Publisher != null)
			{
				result.Publisher = _publisherRepository.Get(result.Publisher.CompanyName);
			}
			if (result.Genres.Count != 0)
			{
				result.Genres = result.Genres.Select(genre => _genreRepository.Get(genre.Name)).ToList();
			}
		}
	}
}