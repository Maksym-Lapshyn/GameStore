using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
    [OutputCache(Duration = 60, VaryByHeader = "get;post")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService service)
        {
            _gameService = service;
        }

        [HttpGet]
        public ActionResult NewGame()
        {
            return View(new GameViewModel());
        }

        [HttpPost]
        public ActionResult NewGame(GameDto game)
        {
            if (!ModelState.IsValid)
            {
                return View(game);
            }

            _gameService.Create(game);
            return Redirect("/games");
        }

        [HttpPost]
        public ActionResult UpdateGame(GameDto game)
        {
            _gameService.Edit(game);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public JsonResult ShowGame(string gameKey)
        {
            GameDto game = _gameService.GetGameByKey(gameKey);
            return Json(game, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListAllGames()
        {
            IEnumerable<GameDto> gameDtos = _gameService.GetAll();
            IEnumerable<GameViewModel> gameViewModels =
                Mapper.Map<IEnumerable<GameDto>, IEnumerable<GameViewModel>>(gameDtos);
            return Json(gameViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteGame(int id)
        {
            _gameService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public FileResult DownloadGame(string gameKey)
        {
            string path = Server.MapPath("~/file.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return new FileContentResult(fileBytes, "application/pdf");
        }
    }
}