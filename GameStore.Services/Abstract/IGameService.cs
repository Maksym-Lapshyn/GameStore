using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IGameService : IService<GameDto>
    {
        IEnumerable<GameDto> GetBy(string genreName);

		IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames);

		GameDto GetSingleBy(string gameKey);
	}
}
