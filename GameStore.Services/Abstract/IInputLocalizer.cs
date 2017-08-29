namespace GameStore.Services.Abstract
{
	public interface IInputLocalizer<T>
	{
		T Localize(string language, T entity);
	}
}