using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfPlatformTypeRepository : IEfPlatformTypeRepository
	{
		private readonly GameStoreContext _context;

		public EfPlatformTypeRepository(GameStoreContext context)
		{
			_context = context;
		}

		public PlatformType Get(string type)
		{
			return _context.PlatformTypes.First(p => p.Type == type);
		}

		public IQueryable<PlatformType> Get()
		{
			return _context.PlatformTypes;
		}

		public bool Contains(string type)
		{
			return _context.PlatformTypes.Any(p => p.Type == type);
		}

		public void Insert(PlatformType platformType)
		{
			_context.PlatformTypes.Add(platformType);
		}
	}
}