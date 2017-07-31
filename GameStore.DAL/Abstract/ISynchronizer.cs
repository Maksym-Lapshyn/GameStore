namespace GameStore.DAL.Abstract
{
	public interface ISynchronizer<T>
	{
		T Synchronize(T entity);
	}
}