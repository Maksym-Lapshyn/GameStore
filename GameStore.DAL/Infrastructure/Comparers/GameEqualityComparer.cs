using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Infrastructure.Comparers
{
	public class GameEqualityComparer : IEqualityComparer<Game>
	{
		public bool Equals(Game x, Game y)
		{
			if (x.NorthwindId != null && y.NorthwindId != null)
			{
				return x.NorthwindId == y.NorthwindId;
			}

			return false;
		}

		public int GetHashCode(Game obj)
		{
			return obj.Key.GetHashCode();
		}
	}
}