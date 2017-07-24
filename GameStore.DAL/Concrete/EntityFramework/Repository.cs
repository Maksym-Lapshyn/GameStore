using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly GameStoreContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public Repository(GameStoreContext context)
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