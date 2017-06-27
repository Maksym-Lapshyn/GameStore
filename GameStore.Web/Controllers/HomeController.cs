using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
		//TODO: Consider: make fields readonly Fixed in ML_2
		private IGameService _gameService;

        public HomeController(IGameService service)
        {
            _gameService = service;
        }

        public ActionResult Index()
        {
            if (!_gameService.GetAll().Any())
			{
                GameDto game = new GameDto { Name = "Test game" };
				_gameService.Create(game);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}