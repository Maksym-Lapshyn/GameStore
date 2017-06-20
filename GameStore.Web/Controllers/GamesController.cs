using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Entities;
using GameStore.Services.Abstract;

namespace GameStore.Web.Controllers
{
    public class GamesController : Controller
    {
        private IGameService _iService;

        public GamesController(IGameService service)
        {
            _iService = service;
        }

        [HttpPost]
        public JsonResult NewGame(Game newGame)
        {
            _iService.CreateNewGame(newGame);
            return null;
        }

        [HttpPost]
        public JsonResult UpdateGame(Game game)
        {
            _iService.EditGame(game);
            return null;
        }

        [HttpGet]
        public JsonResult ShowGame(string key)
        {
            Game game = _iService.GetGameByKey(key);
            return Json(game, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListAllGames()
        {
            IEnumerable<Game> games = _iService.GetAllGames();
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteGame(int gameId)
        {
            _iService.DeleteGame(gameId);
            return null;
        }

        [HttpGet]
        public FileResult DownloadGame()
        {
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            _iService.Dispose();
            base.Dispose(disposing);
        }

    }
}