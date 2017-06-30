using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
    //[OutputCache(Duration = 60, VaryByHeader = "get;post")]
    public class GameController : Controller
    {
		private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;

        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
        }

        [HttpGet]
        public ActionResult NewGame()
        {
            return View(new GameViewModel());
        }

        [HttpPost]
        public ActionResult NewGame(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                gameViewModel.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeDto>, IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
                gameViewModel.Genres = Mapper.Map<IEnumerable<GenreDto>, IEnumerable<GenreViewModel>>(_genreService.GetAll());

                return View(gameViewModel);
            }

            var gameDto = Mapper.Map<GameViewModel, GameDto>(gameViewModel);
            _gameService.Create(gameDto);

            return Redirect("/games");
        }

        [HttpPost]
        public ActionResult UpdateGame(GameDto game)
        {
            _gameService.Edit(game);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ViewResult ShowGame(string gameKey)
        {
            var gameDto = _gameService.GetSingleBy(gameKey);
            var gameViewModel = Mapper.Map<GameDto, GameViewModel>(gameDto);

            return View(gameViewModel);
        }

        public JsonResult ListAllGames()
        {
            var gameDtos = _gameService.GetAll();
            var games = Mapper.Map<IEnumerable<GameDto>, IEnumerable<GameViewModel>>(gameDtos);

            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteGame(int id)
        {
            _gameService.Delete(id);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public FileResult DownloadGame(string gameKey)
        {
            var path = Server.MapPath("~/file.pdf");
            var fileBytes = System.IO.File.ReadAllBytes(path);

            return new FileContentResult(fileBytes, "application/pdf");
        }
    }
}