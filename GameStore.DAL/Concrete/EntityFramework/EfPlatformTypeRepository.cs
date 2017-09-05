using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfPlatformTypeRepository : IEfPlatformTypeRepository
	{
		private readonly GameStoreContext _context;

		public EfPlatformTypeRepository(GameStoreContext context)
		{
			_context = context;
		}

		public PlatformType GetSingle(Expression<Func<PlatformType, bool>> predicate)
		{
			return _context.PlatformTypes.First(predicate);
		}

		public IQueryable<PlatformType> GetAll(Expression<Func<PlatformType, bool>> predicate = null)
		{
			return predicate != null ? _context.PlatformTypes.Where(predicate) : _context.PlatformTypes;
		}

		public bool Contains(Expression<Func<PlatformType, bool>> predicate)
		{
			return _context.PlatformTypes.Any(predicate);
		}

		public void Insert(PlatformType platformType)
		{
			_context.PlatformTypes.Add(platformType);
		}

		public PlatformType GetSingleOrDefault(Expression<Func<PlatformType, bool>> predicate)
		{
			return _context.PlatformTypes.FirstOrDefault(predicate);
		}
	}
}