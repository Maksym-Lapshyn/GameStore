namespace GameStore.DAL.Abstract
{
	public interface ILocalizer<T>
	{
		T Localize(T entity, string language);
	}
}