using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		IEnumerable<GameDto> GetAll(string genreName);

		IEnumerable<GameDto> GetAll(IEnumerable<string> platformTypeNames);

		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingle(string gameKey);

		void Create(GameDto gameDto);

		void Update(GameDto gameDto);

		void Delete(string gameKey);

		IEnumerable<GameDto> GetAll(GameFilterDto gameFilter = null, int? itemsToSkip = null, int? itemsToTake = null);
	}
}