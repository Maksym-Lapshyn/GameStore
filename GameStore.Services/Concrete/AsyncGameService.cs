using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Threading.Tasks;

namespace GameStore.Services.Concrete
{
	public class AsyncGameService : IAsyncGameService
	{
		private readonly IAsyncUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IOutputLocalizer<Game> _outputLocalizer;
		private readonly IAsyncGameRepository _gameRepository;

		public AsyncGameService(IAsyncUnitOfWork unitOfWork,
			IMapper mapper,
			IOutputLocalizer<Game> outputLocalizer,
			IAsyncGameRepository gameRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_outputLocalizer = outputLocalizer;
			_gameRepository = gameRepository;
		}

		public async Task<GameDto> GetSingleOrDefaultAsync(string language, string gameKey)
		{
			var game = await _gameRepository.GetSingleOrDefaultAsync(g => g.Key == gameKey);

			if (game == null)
			{
				return null;
			}

			game.ViewsCount++;

			await _gameRepository.UpdateAsync(game);
			await _unitOfWork.SaveAsync();

			_outputLocalizer.Localize(language, game);

			var gameDto = _mapper.Map<Game, GameDto>(game);

			return gameDto;
		}
	}
}
