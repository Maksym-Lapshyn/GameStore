using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	//TODO: Required: Blank line after each method/property
    public interface IGameService : IService<GameDto>
    {
        IEnumerable<GameDto> GetGamesByGenre(string genreName); //TODO: Consider: Rename to 'GetBy'
		IEnumerable<GameDto> GetGamesByPlatformTypes(IEnumerable<string> platformTypeNames); //TODO: Consider: Rename to 'GetBy'
		GameDto GetGameByKey(string key); //TODO: Consider: Rename to 'GetSingleBy'
	}
}
