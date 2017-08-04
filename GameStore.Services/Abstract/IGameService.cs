using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IGameService : IService
	{
		IEnumerable<GameDto> GetBy(string genreName);

		IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames);

		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingleBy(string gameKey);

		void Create(GameDto gameDto);

		void Update(GameDto gameDto);

		void Delete(string gameKey);

		IEnumerable<GameDto> GetAll(GameFilterDto gameFilter = null, int? skip = null, int? take = null);
	}
}