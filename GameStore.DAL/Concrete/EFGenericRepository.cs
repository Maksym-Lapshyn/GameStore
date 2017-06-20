using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
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

        public void GenericRepository(GameStoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void GenericRepository()
        {
            _context = new GameStoreContext();
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
            }
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
