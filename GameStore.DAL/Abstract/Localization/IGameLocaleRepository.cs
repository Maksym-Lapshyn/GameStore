using GameStore.Common.Entities.Localization;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Localization
{
	public interface IGameLocaleRepository
	{
		IEnumerable<GameLocale> GetAllBy(int gameId);
	}
}