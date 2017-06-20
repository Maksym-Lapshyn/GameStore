using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.Services.Concrete
{
    public class GameServices
    {
        private IUnitOfWork _unitOfWork;

        public GameServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateNewGame(Game newGame)
        {
            _unitOfWork.GameRepository.Insert(newGame);
            _unitOfWork.Save();
        }

        public void EditGame(Game newGame)
        {
            _unitOfWork.GameRepository.Update(newGame);
            _unitOfWork.Save();
        }

        public void DeleteGame(int gameId)
        {
            _unitOfWork.GameRepository.Delete(gameId);
            _unitOfWork.Save();
        }

        public Game GetGameByKey(string key)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Key == key);
            return game;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _unitOfWork.GameRepository.Get();
        }

        public void AddCommentToGame(int gameId, Comment newComment)
        {
            Game game = _unitOfWork.GameRepository.GetById(gameId);
            game.Comments.Add(newComment);
            _unitOfWork.Save();
        }

        public IEnumerable<Comment> GetAllCommentsByGameKey(string key)
        {
            return _unitOfWork.GameRepository.Get().First(g => g.Key == key).Comments;
        }

        public IEnumerable<Game> GetGamesByGenre(string genre)
        {
            return _unitOfWork.GameRepository.Get().Where(game => game.Genres.Any(_genre => _genre.Name == genre));
        }

        public IEnumerable<Game> GetGamesByPlatformTypes(IEnumerable<string> platformTypes)
        {
            IEnumerable<Game> allGames = _unitOfWork.GameRepository.Get();
            List<Game> matchedGames = new List<Game>();
            foreach (Game game in allGames)
            {
                foreach (PlatformType type in game.PlatformTypes)
                {
                    if (platformTypes.Contains(type.Type))
                    {
                        matchedGames.Add(game);
                    }
                }
            }

            return matchedGames;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
