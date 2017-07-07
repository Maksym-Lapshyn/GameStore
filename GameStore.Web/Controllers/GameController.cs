﻿using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
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
		public ActionResult New()
		{
			var gameViewModel = new GameViewModel();
			FillProperties(gameViewModel);

			return View(gameViewModel);
		}

		[HttpPost]
		public ActionResult New(GameViewModel gameViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(gameViewModel);
			}

			var gameDto = Mapper.Map<GameViewModel, GameDto>(gameViewModel);
			_gameService.Create(gameDto);

			return RedirectToAction("ListAll");
		}

		[HttpPost]
		public ActionResult Update(GameDto game)
		{
			_gameService.Edit(game);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		public ActionResult Show(string gameKey)
		{
			var gameDto = _gameService.GetSingleBy(gameKey);
			var gameViewModel = Mapper.Map<GameDto, GameViewModel>(gameDto);

			return View(gameViewModel);
		}

		public ActionResult ListAll()
		{
			var gameDtos = _gameService.GetAll();
			var games = Mapper.Map<List<GameDto>, List<GameViewModel>>(gameDtos.ToList());

			return Json(games, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Delete(int gameId)
		{
			_gameService.Delete(gameId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		public FileResult Download(string gameKey)
		{
			var path = Server.MapPath("~/file.pdf");
		    var fileBytes = new byte[0];
		    if (System.IO.File.Exists(path))
		    {
		        fileBytes = System.IO.File.ReadAllBytes(path);
		    }

			return new FileContentResult(fileBytes, "application/pdf");
		}

		[OutputCache(Duration = 60)]
		public PartialViewResult ShowNumberOfGames()
		{
			var gamesCount = _gameService.GetAll().Count();

			return PartialView(gamesCount);
		}

		private void FillProperties(GameViewModel gameViewModel)
		{
			gameViewModel.PlatformTypesData = 
				Mapper.Map<List<PlatformTypeDto>, List<PlatformTypeViewModel>>(_platformTypeService.GetAll().ToList());
			gameViewModel.GenresData = 
				Mapper.Map<List<GenreDto>, List<GenreViewModel>>(_genreService.GetAll().ToList());
			gameViewModel.PublishersData = 
				Mapper.Map<List<PublisherDto>, List<PublisherViewModel>>(_publisherService.GetAll().ToList());
		}
	}
}