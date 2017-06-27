using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	//TODO: Required: Blank line after each method/property
	public interface IService<TEntity>
    {
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(int id);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
    }
}
