using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;
using GameStore.Web.Infrastructure.Attributes;

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
                GameDto game = new GameDto() { Name = "Test game" };
                _gameService.Create(game);
            }
            else
            {
                throw new NullReferenceException();
            }

            return HttpNotFound();
        }
    }
}