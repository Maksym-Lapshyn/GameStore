using System.Data.Entity;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly GameStoreContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public EfGenericRepository(GameStoreContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}

		public IQueryable<TEntity> Get()
		{
			return _dbSet;
		}

		public TEntity Get(int id)
		{
			var entity = _dbSet.Find(id);

			return entity;
		}

		public void Insert(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void Delete(int id)
		{
			var entity = _dbSet.Find(id);
			entity.IsDeleted = true;
		}

		public void Update(TEntity entityToUpdate)
		{
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}
	}
}