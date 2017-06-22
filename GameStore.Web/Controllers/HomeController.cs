using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;

namespace GameStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGameService _gameService;

        public HomeController(IGameService service)
        {
            _gameService = service;
        }

        public ActionResult Index()
        {
            if (_gameService.GetAll().Count() == 0)
            {
                GameDTO game = new GameDTO(){Name = "Test game"};
                _gameService.Create(game);
            }
            return HttpNotFound();
        }
    }
}