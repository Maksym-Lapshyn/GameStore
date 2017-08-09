using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class GamesController : BaseController
	{
		private const int DefaultPageSize = 10;
		private const int DefaultPage = 1;
		private readonly IGameService _gameService;
		private readonly IGenreService _genreService;
		private readonly IPlatformTypeService _platformTypeService;
		private readonly IPublisherService _publisherService;
		private readonly IMapper _mapper;

		public GamesController(IGameService gameService,
			IGenreService genreService,
			IPlatformTypeService platformTypeService,
			IPublisherService publisherService,
			IMapper mapper)
		{
			_gameService = gameService;
			_genreService = genreService;
			_platformTypeService = platformTypeService;
			_publisherService = publisherService;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult New()
		{
			var gameViewModel = new GameViewModel
			{
				GenresData = GetGenres(),
				PlatformTypesData = GetPlatformTypes(),
				PublishersData = GetPublishers()
			};

			return View(gameViewModel);
		}

		[HttpPost]
		public ActionResult New(GameViewModel gameViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(gameViewModel);
			}

			var gameDto = _mapper.Map<GameViewModel, GameDto>(gameViewModel);
			_gameService.Create(gameDto);

			return RedirectToAction("ListAll");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var gameDto = _gameService.GetSingle(key);
			var gameViewModel = _mapper.Map<GameDto, GameViewModel>(gameDto);
			gameViewModel.GenresData = GetGenres();
			gameViewModel.PublishersData = GetPublishers();
			gameViewModel.PlatformTypesData = GetPlatformTypes();

			return View(gameViewModel);
		}

		[HttpPost]
		public ActionResult Update(GameViewModel gameViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(gameViewModel);
			}

			var gameDto = _mapper.Map<GameViewModel, GameDto>(gameViewModel);
			_gameService.Update(gameDto);

			return RedirectToAction("ListAll");
		}

		public ActionResult Show(string key)
		{
			var gameDto = _gameService.GetSingle(key);
			var gameViewModel = _mapper.Map<GameDto, GameViewModel>(gameDto);

			return View(gameViewModel);
		}

		[HttpGet]
		public ActionResult ListAll(AllGamesViewModel model)
		{
			if (!ModelState.IsValid && model.FilterIsChanged)
			{
				model = UpdateInvalidModel(model);

				return View(model);
			}

			if (model.Filter == null)
			{
				model = UpdateNewModel(model);
				model.FilterState = model.Filter;
			}
			else if (model.FilterIsChanged)
			{
				model.FilterState = model.Filter;
				model = UpdateChangedModel(model);
			}

			ModelState.Clear();//Clears errors if any is present and user did not apply filter

			model.TotalPages = (int)Math.Ceiling((decimal)model.TotalItems / model.PageSize);
			model = UpdatePagination(model);
			model = UpdateGames(model);
			model = UpdateFilterDate(model);

			return View(model);
		}

		[HttpPost]
		public ActionResult Remove(string key)
		{
			_gameService.Delete(key);

			return RedirectToAction("ListAll");
		}

		[OutputCache(Duration = 60)]
		public ActionResult ShowCount()
		{
			var count = _gameService.GetCount();

			return PartialView(count);
		}

		public FileResult Download(string key)
		{
			var path = Server.MapPath("~/file.pdf");
			var fileBytes = new byte[0];

			if (System.IO.File.Exists(path))
			{
				fileBytes = System.IO.File.ReadAllBytes(path);
			}

			return new FileContentResult(fileBytes, "application/pdf");
		}

		private AllGamesViewModel UpdateGames(AllGamesViewModel model)
		{
			var itemsToSkip = model.PageSize * (model.CurrentPage - 1);
			var itemsToTake = model.PageSize;
			model.Games = GetGames(model.FilterState, itemsToSkip, itemsToTake);

			return model;
		}

		private AllGamesViewModel UpdateFilterDate(AllGamesViewModel model)
		{
			model.Filter.PlatformTypesData = GetPlatformTypes();
			model.Filter.GenresData = GetGenres();
			model.Filter.PublishersData = GetPublishers();

			return model;
		}

		private AllGamesViewModel UpdatePagination(AllGamesViewModel model)
		{
			model.StartPage = model.CurrentPage - 5;
			model.EndPage = model.CurrentPage + 4;

			if (model.StartPage <= 0)
			{
				model.EndPage -= model.StartPage - 1;
				model.StartPage = 1;
			}

			if (model.EndPage > model.TotalPages)
			{
				model.EndPage = model.TotalPages;

				if (model.EndPage > 10)
				{
					model.StartPage = model.EndPage - 9;
				}
			}

			return model;
		}

		private AllGamesViewModel UpdateChangedModel(AllGamesViewModel model)
		{
			model.CurrentPage = DefaultPage;
			model.TotalItems = _gameService.GetCount(_mapper.Map<GameFilterViewModel, GameFilterDto>(model.FilterState));
			model = UpdatePagination(model);
			model.FilterIsChanged = false;

			return model;
		}

		private AllGamesViewModel UpdateNewModel(AllGamesViewModel model)
		{
			model.Filter = new GameFilterViewModel();
			model.CurrentPage = DefaultPage;
			model.PageSize = DefaultPageSize;
			model.TotalItems = _gameService.GetCount();
			model = UpdatePagination(model);

			return model;
		}

		private AllGamesViewModel UpdateInvalidModel(AllGamesViewModel model)
		{
			model.FilterIsChanged = false;
			model = UpdateGames(model);

			return model;
		}

		private List<GameViewModel> GetGames(GameFilterViewModel filter, int itemsToSkip, int itemsToTake)
		{
			var filterDto = _mapper.Map<GameFilterViewModel, GameFilterDto>(filter);

			return _mapper.Map<IEnumerable<GameDto>, List<GameViewModel>>(_gameService.GetAll(filterDto, itemsToSkip, itemsToTake));
		}

		private List<PlatformTypeViewModel> GetPlatformTypes()
		{
			return _mapper.Map<IEnumerable<PlatformTypeDto>, List<PlatformTypeViewModel>>(_platformTypeService.GetAll());
		}

		private List<GenreViewModel> GetGenres()
		{
			return _mapper.Map<IEnumerable<GenreDto>, List<GenreViewModel>>(_genreService.GetAll());
		}

		private List<PublisherViewModel> GetPublishers()
		{
			return _mapper.Map<IEnumerable<PublisherDto>, List<PublisherViewModel>>(_publisherService.GetAll());
		}
	}
}