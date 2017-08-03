namespace GameStore.DAL.Abstract
{
	public interface ILogger<T>
	{
		void LogChange(ILogContainer<T> container);
	}
}