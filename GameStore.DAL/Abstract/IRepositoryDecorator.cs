using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{
	public interface IGenericRepositoryDecorator<TEntity> : IGenericRepository<TEntity> where TEntity:BaseEntity
	{
	}
}