using GameStore.Common.Entities;

namespace GameStore.DAL.Abstract
{
	public interface ICopier<T> where T : BaseEntity
	{
		T Copy(T entity);
	}
}