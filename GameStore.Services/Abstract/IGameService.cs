using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingle(string gameKey, string language);

		void Create(GameDto gameDto);

		void Update(GameDto gameDto);

		void Delete(string gameKey);

		IEnumerable<GameDto> GetAll(string language, GameFilterDto gameFilter = null, int? itemsToSkip = null, int? itemsToTake = null, bool allowDeleted = false);

		bool Contains(string gameKey);
	}
}