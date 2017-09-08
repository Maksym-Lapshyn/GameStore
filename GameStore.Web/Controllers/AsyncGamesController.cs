using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class AsyncGamesController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly IAsyncGameService _gameService;

		public AsyncGamesController(IAsyncGameService gameService,
			IMapper mapper,
			IAuthentication authentication)
			: base(authentication)
		{
			_gameService = gameService;
			_mapper = mapper;
		}

		public async Task<ActionResult> Show(string key)
		{
			var gameDto = await _gameService.GetSingleOrDefaultAsync(CurrentLanguage, key);
			var gameViewModel = _mapper.Map<GameDto, GameViewModel>(gameDto);

			return View("Show", gameViewModel);
		}
	}
}