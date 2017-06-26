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
            if (_unitOfWork.GameRepository.Get().All(g => g.Key != game.Key))
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
            Game game = _unitOfWork.GameRepository.Get().FirstOrDefault(g => g.Id == gameDto.Id);
            if (game != null)
            {
                game = Mapper.Map(gameDto, game);
                _unitOfWork.GameRepository.Update(game);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("There is no such game");
            }
        }

        public void Delete(int id)
        {
            Game gameToRemove = _unitOfWork.GameRepository.Get().FirstOrDefault(g => g.Id == id);
            if (gameToRemove != null)
            {
                _unitOfWork.GameRepository.Delete(id);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("There is no such game");
            }
        }

        public GameDto Get(int id)
        {
            Game game = _unitOfWork.GameRepository.Get().FirstOrDefault(g => g.Id == id);
            if (game != null)
            {
                GameDto gameDto = Mapper.Map<Game, GameDto>(game);
                return gameDto;
            }

            throw new ArgumentException("There is no game with such id");
        }

        public GameDto GetGameByKey(string key)
        {
            Game game = _unitOfWork.GameRepository.Get().FirstOrDefault(g => g.Key.ToLower() == key.ToLower());
            if (game != null)
            {
                GameDto gameDto = Mapper.Map<Game, GameDto>(game);
                return gameDto;
            }

            throw new ArgumentException("There is no game with such key");
        }

        public IEnumerable<GameDto> GetAll()
        {
            IEnumerable<Game> games = _unitOfWork.GameRepository.Get();
            IEnumerable<GameDto> gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);
            return gameDtOs;
        }

        public IEnumerable<GameDto> GetGamesByGenre(string genreName)
        {
            IEnumerable<Game> games = _unitOfWork.GameRepository.Get().Where(game => game.Genres.Any(genre => genre.Name == genreName));
            IEnumerable<GameDto> gameDtOs = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);
            return gameDtOs;
        }

        public IEnumerable<GameDto> GetGamesByPlatformTypes(IEnumerable<string> platformTypeNames)
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
