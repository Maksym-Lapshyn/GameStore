using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
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
		private readonly IPipeline<IQueryable<Game>> _pipeline;

		public GameService(IUnitOfWork unitOfWork, IMapper mapper, IPipeline<IQueryable<Game>> pipeline)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_pipeline = pipeline;
		}

		public void Create(GameDto gameDto)
		{
			var game = _mapper.Map<GameDto, Game>(gameDto);
			Map(gameDto, game);
			game.ViewsCount = 0;
			game.DateAdded = DateTime.UtcNow;
			_unitOfWork.GameRepository.Insert(game);
			_unitOfWork.Save();
		}

		public void Edit(GameDto gameDto)
		{
			var game = _unitOfWork.GameRepository.Get().First(g => g.Id == gameDto.Id);
			game = _mapper.Map(gameDto, game);
			Map(gameDto, game);
			_unitOfWork.GameRepository.Update(game);
			_unitOfWork.Save();
		}

		public void SaveView(int gameId)
		{
			var game = _unitOfWork.GameRepository.Get(gameId);
			game.ViewsCount++;
			_unitOfWork.GameRepository.Update(game);
			_unitOfWork.Save();
		}

		public void Delete(int gameId)
		{
			_unitOfWork.GameRepository.Delete(gameId);
			_unitOfWork.Save();
		}

		public GameDto GetSingleBy(int id)
		{
			var game = _unitOfWork.GameRepository.Get().First(g => g.Id == id);
			var gameDto = _mapper.Map<Game, GameDto>(game);

			return gameDto;
		}

		public GameDto GetSingleBy(string gameKey)
		{
			var game = _unitOfWork.GameRepository.Get().First(g => g.Key.ToLower() == gameKey.ToLower());
			var gameDto = _mapper.Map<Game, GameDto>(game);

			return gameDto;
		}

		public IEnumerable<GameDto> GetAll(FilterDto filter = null, int? skip = null, int? take = null)
		{
			var games = _unitOfWork.GameRepository.Get();

			if (filter != null)
			{
				var filterMapper = new FilterMapper();
				filterMapper.Map(filter).ForEach(f => _pipeline.Register(f));
				games = _pipeline.Process(games);
			}

			if (skip != null && take != null)
			{
				games = games.Skip(skip.Value).Take(take.Value);
			}

			var gameDtos = _mapper.Map<IQueryable<Game>, IEnumerable<GameDto>>(games);

			return gameDtos;
		}

		public IEnumerable<GameDto> GetBy(string genreName)
		{
			var games = _unitOfWork.GameRepository
				.Get().Where(game => game.Genres.Any(genre => genre.Name.ToLower() == genreName.ToLower()));
			var gameDtOs = _mapper.Map<IQueryable<Game>, IEnumerable<GameDto>>(games);

			return gameDtOs;
		}

		public IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames)
		{
			var allGames = _unitOfWork.GameRepository.Get();
			var matchedGames = (from game in allGames from type in game.PlatformTypes where platformTypeNames.Contains(type.Type) select game);
			var gameDtOs = _mapper.Map<IQueryable<Game>, IEnumerable<GameDto>>(matchedGames);

			return gameDtOs;
		}

		private void Map(GameDto input, Game result)
		{
			result.Publisher = _unitOfWork.PublisherRepository.Get(input.PublisherInput);
			result.PlatformTypes = input.PlatformTypesInput.Select(id => _unitOfWork.PlatformTypeRepository.Get(id)).ToList();
			result.Genres = input.GenresInput.Select(id => _unitOfWork.GenreRepository.Get(id)).ToList();
		}
	}
}