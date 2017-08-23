using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Common.Infrastructure.Comparers
{
	public class GenreEqualityComparer : IEqualityComparer<Genre>
	{
		public bool Equals(Genre x, Genre y)
		{
			if (x.NorthwindId != null && y.NorthwindId != null)
			{
				return x.NorthwindId == y.NorthwindId;
			}

			return false;
		}

		public int GetHashCode(Genre obj)
		{
			return obj.Name.GetHashCode();
		}
	}
}