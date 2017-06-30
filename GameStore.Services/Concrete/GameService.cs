using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using AutoMapper;

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

        public GameDto Get(int id)
        {
            var game = _unitOfWork.GameRepository.Get().First(g => g.Id == id);
            var gameDto = Mapper.Map<Game, GameDto>(game);

            return gameDto;
        }

        public GameDto GetSingleBy(string gameKey)
        {
            var game = _unitOfWork.GameRepository.Get().First(g => string.Equals(g.Key, gameKey, StringComparison.CurrentCultureIgnoreCase));
            var gameDto = Mapper.Map<Game, GameDto>(game);

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
            var matchedGames = new List<Game>();
            foreach (var game in allGames)
            {
                foreach (var type in game.PlatformTypes)
                {
                    if (platformTypeNames.Contains(type.Type))
                    {
                        matchedGames.Add(game);
                    }
                }
            }

            var gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(matchedGames);

            return gameDtOs;
        }
    }
}
