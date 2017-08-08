using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IGameRepository
	{
		IEnumerable<Game> GetAll(GameFilter filter = null, int? skip = null, int? take = null);

		Game GetSingle(string gameKey);

		void Insert(Game game);

		void Delete(string gameKey);

		void Update(Game game);

		bool Contains(string gameKey);
	}
}