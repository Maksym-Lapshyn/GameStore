using System.Threading.Tasks;

namespace GameStore.DAL.Abstract.Common
{
	public interface IAsyncUnitOfWork
	{
		Task SaveAsync();
	}
}