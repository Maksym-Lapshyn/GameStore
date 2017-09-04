using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingle(string language, string gameKey);

		void Create(string language, GameDto gameDto);

		void Update(string language, GameDto gameDto);

		void Delete(string gameKey);

		IEnumerable<GameDto> GetAll(string language, GameFilterDto gameFilter = null, int? itemsToSkip = null, int? itemsToTake = null, bool allowDeleted = false);

		bool Contains(string gameKey);
	}
}