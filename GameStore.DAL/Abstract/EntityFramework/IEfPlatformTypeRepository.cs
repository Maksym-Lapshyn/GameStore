using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPlatformTypeRepository
	{
		PlatformType Get(string type);

		IQueryable<PlatformType> Get();
	}
}