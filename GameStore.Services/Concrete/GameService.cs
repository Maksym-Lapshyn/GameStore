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
            Game game = Mapper.Map<GameDto, Game>(gameDto);
            bool gameKeyIsReserved = _unitOfWork.GameRepository.Get().All(g => g.Key != game.Key);
            if (gameKeyIsReserved)
			{
                _unitOfWork.GameRepository.Insert(game);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("There is a game with such key already");
            }
        }

        public void Edit(GameDto gameDto)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Id == gameDto.Id);
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
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Id == id);
            GameDto gameDto = Mapper.Map<Game, GameDto>(game);

            return gameDto;
        }

        public GameDto GetSingleBy(string gameKey)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => string.Equals(g.Key, gameKey, StringComparison.CurrentCultureIgnoreCase));
            GameDto gameDto = Mapper.Map<Game, GameDto>(game);

            return gameDto;
        }

        public IEnumerable<GameDto> GetAll()
        {
            IEnumerable<Game> games = _unitOfWork.GameRepository.Get();
            IEnumerable<GameDto> gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);

            return gameDtOs;
        }

        public IEnumerable<GameDto> GetBy(string genreName)
        {
            IEnumerable<Game> games = _unitOfWork.GameRepository.Get().Where(game => game.Genres.Any(genre => genre.Name == genreName));
            IEnumerable<GameDto> gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);

            return gameDtOs;
        }

        public IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames)
        {
            IEnumerable<Game> allGames = _unitOfWork.GameRepository.Get();
            List<Game> matchedGames = new List<Game>();
            foreach (Game game in allGames)
            {
                foreach (PlatformType type in game.PlatformTypes)
                {
                    if (platformTypeNames.Contains(type.Type))
                    {
                        matchedGames.Add(game);
                    }
                }
            }
            IEnumerable<GameDto> gameDtOs = Mapper.Map<IEnumerable<Game>, List<GameDto>>(matchedGames);

            return gameDtOs;
        }
    }
}
