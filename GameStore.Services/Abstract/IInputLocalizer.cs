namespace GameStore.Services.Abstract
{
	public interface IInputLocalizer<in T>
	{
		void Localize(string language, T entity);
	}
}