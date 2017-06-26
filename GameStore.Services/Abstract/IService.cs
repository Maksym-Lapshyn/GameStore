using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
    public interface IService<TEntity>
    {
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(int id);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
    }
}
