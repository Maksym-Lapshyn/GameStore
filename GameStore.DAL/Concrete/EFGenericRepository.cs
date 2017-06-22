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
using GameStore.Domain.Abstract;

namespace GameStore.DAL.Concrete
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, ISoftDeletable
    {
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
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public TEntity GetById(int? id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
            {
                return _dbSet.Find(id);
            }
            else
            {
                throw new ArgumentNullException("There is no such entity");
            }
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            TEntity entityToRemove = _dbSet.Find(entity);
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
