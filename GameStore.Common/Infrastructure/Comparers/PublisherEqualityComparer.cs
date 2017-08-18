using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Common.Infrastructure.Comparers
{
	public class PublisherEqualityComparer : IEqualityComparer<Publisher>
	{
		public bool Equals(Publisher x, Publisher y)
		{
			if (x.NorthwindId != null && y.NorthwindId != null)
			{
				return x.NorthwindId == y.NorthwindId;
			}

			return false;
		}

		public int GetHashCode(Publisher obj)
		{
			return obj.CompanyName.GetHashCode();
		}
	}
}