using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete
{
	//TODO: Required: Remove 'Ef' prefix
	//TODO: Consider: Remove 'class' restriction
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, ISoftDeletable
    {
		//TODO: Consider: make fields readonly
        private GameStoreContext _context;
        private DbSet<TEntity> _dbSet;

        public EFGenericRepository(GameStoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)// TODO: Consider: Use ternary operator
			{
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

		//TODO: Required: Use First() instead of Find(), null check and exception throw
		//TODO: Consider: make parameter not nullable
		public TEntity GetById(int? id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
            {
                return _dbSet.Find(id);
            }
            else //TODO: Required: Remove redundant 'else'
            {
                throw new ArgumentNullException("There is no such entity");
            }
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

		//TODO: Required: Use First() instead of Find(), null check and exception throw
		public void Delete(int id)
        {
            TEntity entityToRemove = _dbSet.Find(id);
            if (entityToRemove != null)
            {
                entityToRemove.IsDeleted = true;
            }
            else
            {
                throw new ArgumentNullException("There is no such entity");
            }
        }

        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("Entity is null");
            }

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
