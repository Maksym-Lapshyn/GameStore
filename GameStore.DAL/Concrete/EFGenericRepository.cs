using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Concrete
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private GameStoreContext _context;
        private DbSet<TEntity> dbSet;

        public void GenericRepository(GameStoreContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return dbSet;
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
