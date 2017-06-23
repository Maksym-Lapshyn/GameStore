using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, ISoftDeletable
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        TEntity GetById(int? id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Update(TEntity entityToUpdate);
    }
}