using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		IEnumerable<GameDto> GetBy(string genreName);

		IEnumerable<GameDto> GetBy(IEnumerable<string> platformTypeNames);

		GameDto GetSingleBy(string gameKey);

		void Create(GameDto entity);

		void Edit(GameDto entity);

		void Delete(int id);

		GameDto GetSingleBy(int gameId);

		IEnumerable<GameDto> GetAll();
	}
}
