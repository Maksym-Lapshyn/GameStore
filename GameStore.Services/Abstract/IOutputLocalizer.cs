namespace GameStore.Services.Abstract
{
	public interface IOutputLocalizer<in T>
	{
		void Localize(string language, T entity);
	}
}