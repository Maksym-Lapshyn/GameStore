using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		IEnumerable<GameDto> GetBy(string genreName);

		IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames);

		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingleBy(string gameKey);

		void SaveView(string gameKey);

		void Create(GameDto gameDto);

		void Edit(GameDto gameDto);

		void Delete(string gameKey);

		GameDto GetSingleBy(int gameId);

		IEnumerable<GameDto> GetAll(GameFilterDto gameFilter = null, int? skip = null, int? take = null);
	}
}