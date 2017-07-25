namespace GameStore.Services.Abstract
{
	public interface IFilter<T>
	{
		T Execute(T input);
	}
}