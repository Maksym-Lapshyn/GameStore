using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IPlatformTypeRepository
	{
		PlatformType Get(string type);

		IEnumerable<PlatformType> Get();

		bool Contains(string type);

		void Insert(PlatformType platformType);
	}
}
