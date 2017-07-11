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

		public GameService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Create(GameDto gameDto)
		{
			var game = Mapper.Map<GameDto, Game>(gameDto);
			Map(gameDto, game);
			_unitOfWork.GameRepository.Insert(game);
			_unitOfWork.Save();
		}

		public void Edit(GameDto gameDto)
		{
			var game = _unitOfWork.GameRepository.Get(g => g.Id == gameDto.Id).First();
			game = Mapper.Map(gameDto, game);
			Map(gameDto, game);
			_unitOfWork.GameRepository.Update(game);
			_unitOfWork.Save();
		}

		public void Delete(int id)
		{
			_unitOfWork.GameRepository.Delete(id);
			_unitOfWork.Save();
		}

		public GameDto GetSingleBy(int id)
		{
			var game = _unitOfWork.GameRepository.Get(g => g.Id == id).First();
			var gameDto = Mapper.Map<Game, GameDto>(game);

			return gameDto;
		}

		public GameDto GetSingleBy(string gameKey)
		{
			var game = _unitOfWork.GameRepository
				.Get(g => string.Equals(g.Key, gameKey, StringComparison.CurrentCultureIgnoreCase)).First();
			var gameDto = Mapper.Map<Game, GameDto>(game);
			gameDto.PublishersData = new List<PublisherDto>
			{
				Mapper.Map<Publisher, PublisherDto>(game.Publisher)
			};

			return gameDto;
		}

		public IEnumerable<GameDto> GetAll()
		{
			var games = _unitOfWork.GameRepository.Get();
			var gameDtos = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games).ToList();
		   
			return gameDtos;
		}

		public IEnumerable<GameDto> GetBy(string genreName)
		{
			var games = _unitOfWork.GameRepository
				.Get(game => game.Genres.Any(genre => string.Equals(genre.Name, genreName, StringComparison.CurrentCultureIgnoreCase)));
			var gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);

			return gameDtOs;
		}

		public IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames)
		{
			var allGames = _unitOfWork.GameRepository.Get();
			var matchedGames = (from game in allGames from type in game.PlatformTypes where platformTypeNames.Contains(type.Type) select game).ToList();
			var gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(matchedGames);

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
