using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{
	public interface ICloner<T> where T : BaseEntity
	{
		T Clone(T entity);
	}
}