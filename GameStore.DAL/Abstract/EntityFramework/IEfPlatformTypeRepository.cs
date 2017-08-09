using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPlatformTypeRepository
	{
		PlatformType GetSingle(string type);

		IQueryable<PlatformType> GetAll();

		bool Contains(string type);

		void Insert(PlatformType platformType);
	}
}