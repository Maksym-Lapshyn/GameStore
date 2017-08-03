using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IGameRepository
	{
		IEnumerable<Game> Get(GameFilter filter = null);

		Game Get(string gameKey);

		void Insert(Game game);

		void Delete(string gameKey);

		void Update(Game game);

		bool Contains(string gameKey);
	}
}
