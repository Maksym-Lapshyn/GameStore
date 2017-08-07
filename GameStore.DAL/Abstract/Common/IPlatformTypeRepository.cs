using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IPlatformTypeRepository
	{
		PlatformType GetSingle(string type);

		IEnumerable<PlatformType> GetAll();

		bool Contains(string type);

		void Insert(PlatformType platformType);
	}
}
