using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		IEnumerable<GameDto> GetBy(string genreName);

		IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames);

		GameDto GetSingleBy(string gameKey);

		void SaveView(int gameId);

		void Create(GameDto gameDto);

		void Edit(GameDto gameDto);

		void Delete(int gameId);

		GameDto GetSingleBy(int gameId);

		IEnumerable<GameDto> GetAll(FilterDto filter = null, int? skip = null, int? take = null);
	}
}