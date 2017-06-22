using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;
using System.IO;

namespace GameStore.Web.Controllers
{
    //[OutputCache(Duration = 60, VaryByHeader = "get;post")]
    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService service)
        {
            _gameService = service;
        }

        //tested
        [HttpPost]
        public ActionResult NewGame(GameDTO newGame)
        {
            _gameService.Create(newGame);
            return Redirect("https://youtube.com");
        }

        //tested
        [HttpPost]
        public ActionResult UpdateGame(GameDTO game)
        {
            _gameService.Edit(game);
            return Redirect("https://youtube.com");
        }

        //tested
        [HttpGet]
        public JsonResult ShowGame(string gameKey)
        {
            GameDTO game = _gameService.GetGameByKey(gameKey);
            return Json(game, JsonRequestBehavior.AllowGet);
        }

        //tested
        [HttpGet]
        public JsonResult ListAllGames()
        {
            IEnumerable<GameDTO> games = _gameService.GetAll();
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        //tested
        [HttpPost]
        public ActionResult DeleteGame(int id)
        {
            _gameService.Delete(id);
            return Redirect("https://youtube.com");
        }

        [HttpGet]
        public FileResult DownloadGame()
        {
            return null;
        }

    }
}