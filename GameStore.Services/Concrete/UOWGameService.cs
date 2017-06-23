using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.Services.Infrastructure;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using AutoMapper;

namespace GameStore.Services.Concrete
{
    public class UowGameService : IGameService
    {
        private IUnitOfWork _unitOfWork;

        public UowGameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(GameDto gameDTO)
        {
            Game game = AutoMapperFactory.CreateGame(gameDTO);
            if (_unitOfWork.GameRepository.Get().All(g => g.Name != game.Name))
            {
                _unitOfWork.GameRepository.Insert(game);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("There is a game with such key already");
            }
        }

        public void Edit(GameDto newGame)
        {
            Game oldGame = _unitOfWork.GameRepository.GetById(newGame.Id);
            if (oldGame != null)
            {
                oldGame = Mapper.Map(newGame, oldGame);
                _unitOfWork.GameRepository.Update(oldGame);
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
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Id == id);
            if (game != null)
            {
                GameDto gameDTO = AutoMapperFactory.CreateGameDto(game);
                return gameDTO;
            }

            throw new ArgumentException("There is no game with such id");
        }

        public GameDto GetGameByKey(string key)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Key.ToLower() == key.ToLower());
            if (game != null)
            {
                GameDto gameDTO = AutoMapperFactory.CreateGameDto(game);
                return gameDTO;
            }

            throw new ArgumentException("There is no game with such key");
        }

        public IEnumerable<GameDto> GetAll()
        {
            IEnumerable<Game> games = _unitOfWork.GameRepository.Get();
            var gameDTOs = AutoMapperFactory.CreateGameDtos(games);
            return gameDTOs;
        }

        

        public IEnumerable<GameDto> GetGamesByGenre(string genreName)
        {
            IEnumerable<Game> games =  _unitOfWork.GameRepository.Get().Where(game => game.Genres.Any(genre => genre.Name == genreName));
            IEnumerable<GameDto> gameDTOs = AutoMapperFactory.CreateGameDtos(games);
            return gameDTOs;
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

            IEnumerable<GameDto> gameDTOs = Mapper.Map<IEnumerable<Game>, List<GameDto>>(matchedGames);
            return gameDTOs;
        }
    }
}
