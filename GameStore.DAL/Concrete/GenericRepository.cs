using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //TODO: Consider: make fields readonly Fixed in ML_2
        private readonly GameStoreContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(GameStoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;
            query = filter != null ? query.Where(filter) : query;
            query = orderBy != null ? orderBy(query) : query;

            return query.ToList();
        }

        public TEntity GetById(int id)
        {
            TEntity entity = _dbSet.First(e => e.Id == id);

            return entity;
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entityToRemove = _dbSet.First(e => e.Id == id);
            entityToRemove.IsDeleted = true;
        }

        public void Update(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
