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
            game.Publisher = _unitOfWork.PublisherRepository.GetById(gameDto.PublisherInput);
            game.PlatformTypes = gameDto.PlatformTypesInput.Select(id => _unitOfWork.PlatformTypeRepository.GetById(id)).ToList();
            game.Genres = gameDto.GenresInput.Select(id => _unitOfWork.GenreRepository.GetById(id)).ToList();
            _unitOfWork.GameRepository.Insert(game);
            _unitOfWork.Save();
        }

        public void Edit(GameDto gameDto)
        {
            var game = _unitOfWork.GameRepository.Get().First(g => g.Id == gameDto.Id);
            game = Mapper.Map(gameDto, game);
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
            var game = _unitOfWork.GameRepository.Get().First(g => g.Id == id);
            var gameDto = Mapper.Map<Game, GameDto>(game);

            return gameDto;
        }

        public GameDto GetSingleBy(string gameKey)
        {
            var game = _unitOfWork.GameRepository.Get().First(g => string.Equals(g.Key, gameKey, StringComparison.CurrentCultureIgnoreCase));
            var gameDto = Mapper.Map<Game, GameDto>(game);
            gameDto.GenresData = Mapper.Map<List<Genre>, List<GenreDto>>(game.Genres.ToList());
            gameDto.PlatformTypesData = Mapper.Map<List<PlatformType>, List<PlatformTypeDto>>(game.PlatformTypes.ToList());
            gameDto.PublishersData = new List<PublisherDto> { Mapper.Map<Publisher, PublisherDto>(game.Publisher) };

            return gameDto;
        }

        public IEnumerable<GameDto> GetAll()
        {
            var games = _unitOfWork.GameRepository.Get();
            var gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);

            return gameDtOs;
        }

        public IEnumerable<GameDto> GetBy(string genreName)
        {
            var games = _unitOfWork.GameRepository.Get().Where(game => game.Genres.Any(genre => genre.Name == genreName));
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
    }
}
