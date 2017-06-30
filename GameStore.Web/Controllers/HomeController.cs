using System.Linq;
using System.Net;
using System.Web.Mvc;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;

namespace GameStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;

        public HomeController(IGameService service)
        {
            _gameService = service;
        }

        public ActionResult Index()
        {
            if (!_gameService.GetAll().Any())
            {
                var game = new GameDto { Name = "Test game" };
                _gameService.Create(game);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}