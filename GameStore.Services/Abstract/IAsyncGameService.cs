using GameStore.Services.Dtos;
using System.Threading.Tasks;

namespace GameStore.Services.Abstract
{
	public interface IAsyncGameService
	{
		Task<GameDto> GetSingleOrDefaultAsync(string language, string gameKey);
	}
}