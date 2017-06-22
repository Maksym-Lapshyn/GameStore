using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IGameService : IService<GameDTO>
    {
        IEnumerable<GameDTO> GetGamesByGenre(string genreName);
        IEnumerable<GameDTO> GetGamesByPlatformTypes(IEnumerable<string> platformTypeNames);
        GameDTO GetGameByKey(string key);
    }
}
