namespace GameStore.DAL.Abstract
{
	public interface IOutputLocalizer<T>
	{
		T Localize(string language, T entity);
	}
}