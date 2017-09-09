using System.Threading.Tasks;

namespace GameStore.DAL.Abstract
{
    public interface IAsyncLogger<T>
    {
        Task LogChangeAsync(ILogContainer<T> container);
    }
}