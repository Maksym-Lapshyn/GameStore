using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;

namespace GameStore.Services.Abstract
{
    public interface IGameService
    {
        void CreateNewGame(Game game);
        void EditGame(Game game);
        void DeleteGame(int id);
        Game GetGameByKey(string key);
        IEnumerable<Game> GetAllGames();
        void AddCommentToGame(string gameKey, Comment comment);
        IEnumerable<Comment> GetAllCommentsByGameKey(string key);
        IEnumerable<Game> GetGamesByGenre(string genreName);
        IEnumerable<Game> GetGamesByPlatformTypes(IEnumerable<string> platformTypeNames);
        void Dispose();
    }
}
