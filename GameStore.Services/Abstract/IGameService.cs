using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingle(string gameKey);

		void Create(GameDto gameDto);

		void Update(GameDto gameDto);

		void Delete(string gameKey);

		IEnumerable<GameDto> GetAll(GameFilterDto gameFilter = null, int? itemsToSkip = null, int? itemsToTake = null);

		bool Contains(string gameKey);
	}
}