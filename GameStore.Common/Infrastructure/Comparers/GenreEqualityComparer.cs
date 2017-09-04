using GameStore.Common.Entities;
using System.Collections.Generic;
using System.Linq;

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
			if (obj.GenreLocales.Count == 0)
			{
				return obj.Name.GetHashCode();
			}

			var genreLocale = obj.GenreLocales.First();

			return genreLocale.Name.GetHashCode();
		}
	}
}