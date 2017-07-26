namespace GameStore.DAL.Abstract
{
	public interface IFilter<T>
	{
		T Execute(T input);
	}
}