using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IGameService : IService<GameDto>
    {
        IEnumerable<GameDto> GetGamesByGenre(string genreName);
        IEnumerable<GameDto> GetGamesByPlatformTypes(IEnumerable<string> platformTypeNames);
        GameDto GetGameByKey(string key);
    }
}
