using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPublisherService _publisherService;

        public GameController(IGameService gameService, IGenreService genreService,
            IPlatformTypeService platformTypeService, IPublisherService publisherService)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
        }

        [HttpGet]
        public ActionResult NewGame()
        {
            var gameViewModel = new GameViewModel();
            FillProperties(gameViewModel);

            return View(gameViewModel);
        }

        [HttpPost]
        public ActionResult NewGame(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                FillProperties(gameViewModel);

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
            var games = Mapper.Map<List<GameDto>, List<GameViewModel>>(gameDtos.ToList());

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

        private void FillProperties(GameViewModel gameViewModel)
        {
            gameViewModel.AllPlatforms = Mapper.Map<List<PlatformTypeDto>, List<PlatformTypeViewModel>>(_platformTypeService.GetAll().ToList());
            gameViewModel.AllGenres = Mapper.Map<List<GenreDto>, List<GenreViewModel>>(_genreService.GetAll().ToList());
            gameViewModel.AllPublishers = Mapper.Map<List<PublisherDto>, List<PublisherViewModel>>(_publisherService.GetAll().ToList());
        }
    }
}