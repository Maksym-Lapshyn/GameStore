using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
			IMapper mapper,
			IAuthentication authentication)
			: base(authentication)
		{
			_gameService = gameService;
			_genreService = genreService;
			_platformTypeService = platformTypeService;
			_publisherService = publisherService;
			_mapper = mapper;
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpGet]
		public ActionResult New()
		{
			var gameViewModel = new GameViewModel
			{
				GenresData = GetGenres(),
				PlatformTypesData = GetPlatformTypes(),
				PublisherData = GetPublishers()
			};

			return View(gameViewModel);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpPost]
		public ActionResult New(GameViewModel model)
		{
			CheckIfKeyIsUnique(model);
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var gameDto = _mapper.Map<GameViewModel, GameDto>(model);
			_gameService.Create(CurrentLanguage, gameDto);

			return RedirectToAction("ShowAll");
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpGet]
		public ActionResult Update(string key)
		{
			var gameDto = _gameService.GetSingle(key, CurrentLanguage);
			var model = _mapper.Map<GameDto, GameViewModel>(gameDto);
			model.GenresData = GetGenres();
			model.PublisherData = GetPublishers();
			model.PlatformTypesData = GetPlatformTypes();

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpPost]
		public ActionResult Update(GameViewModel model)
		{
			CheckIfKeyIsUnique(model);
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var gameDto = _mapper.Map<GameViewModel, GameDto>(model);
			_gameService.Update(CurrentLanguage, gameDto);

			return RedirectToAction("ShowAll");
		}

		public ActionResult Show(string key)
		{
			var gameDto = _gameService.GetSingle(CurrentLanguage, key);
			var gameViewModel = _mapper.Map<GameDto, GameViewModel>(gameDto);

			return View(gameViewModel);
		}

		public ActionResult ShowAll(CompositeGamesViewModel model)
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

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpPost]
		public ActionResult Delete(string key)
		{
			_gameService.Delete(key);

			return RedirectToAction("ShowAll", "Games");
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

		private CompositeGamesViewModel UpdateGames(CompositeGamesViewModel model)
		{
			var itemsToSkip = model.PageSize * (model.CurrentPage - 1);
			var itemsToTake = model.PageSize;
			model.Games = GetGames(model.FilterState, itemsToSkip, itemsToTake);

			return model;
		}

		private CompositeGamesViewModel UpdateFilterDate(CompositeGamesViewModel model)
		{
			model.Filter.PlatformTypesData = GetPlatformTypes();
			model.Filter.GenresData = GetGenres();
			model.Filter.PublishersData = GetPublishers();

			return model;
		}

		private CompositeGamesViewModel UpdatePagination(CompositeGamesViewModel model)
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

		private CompositeGamesViewModel UpdateChangedModel(CompositeGamesViewModel model)
		{
			model.CurrentPage = DefaultPage;
			model.TotalItems = _gameService.GetCount(_mapper.Map<GameFilterViewModel, GameFilterDto>(model.FilterState));
			model = UpdatePagination(model);
			model.FilterIsChanged = false;

			return model;
		}

		private CompositeGamesViewModel UpdateNewModel(CompositeGamesViewModel model)
		{
			model.Filter = new GameFilterViewModel();
			model.CurrentPage = DefaultPage;
			model.PageSize = DefaultPageSize;
			model.TotalItems = _gameService.GetCount();
			model = UpdatePagination(model);

			return model;
		}

		private CompositeGamesViewModel UpdateInvalidModel(CompositeGamesViewModel model)
		{
			model.FilterIsChanged = false;
			model = UpdateGames(model);

			return model;
		}

		private List<GameViewModel> GetGames(GameFilterViewModel filter, int itemsToSkip, int itemsToTake)
		{
			var filterDto = _mapper.Map<GameFilterViewModel, GameFilterDto>(filter);

			return _mapper.Map<IEnumerable<GameDto>, List<GameViewModel>>(User.Identity.IsAuthenticated && CurrentUser.Roles.Any(r => r.AccessLevel == AccessLevel.Manager) 
				? _gameService.GetAll(CurrentLanguage, filterDto, itemsToSkip, itemsToTake, true)
				: _gameService.GetAll(CurrentLanguage, filterDto, itemsToSkip, itemsToTake));
		}

		private List<PlatformTypeViewModel> GetPlatformTypes()
		{
			return _mapper.Map<IEnumerable<PlatformTypeDto>, List<PlatformTypeViewModel>>(_platformTypeService.GetAll(CurrentLanguage));
		}

		private List<GenreViewModel> GetGenres()
		{
			return _mapper.Map<IEnumerable<GenreDto>, List<GenreViewModel>>(_genreService.GetAll(CurrentLanguage));
		}

		private List<PublisherViewModel> GetPublishers()
		{
			return _mapper.Map<IEnumerable<PublisherDto>, List<PublisherViewModel>>(_publisherService.GetAll(CurrentLanguage));
		}

		private void CheckIfKeyIsUnique(GameViewModel model)
		{
			if (!_gameService.Contains(model.Key))
			{
				return;
			}

			var existingGame = _gameService.GetSingle(CurrentLanguage, model.Key);

			if (existingGame.Id != model.Id)
			{
				ModelState.AddModelError("Key", GlobalResource.GameWithSuchKeyAlreadyExists);
			}
		}
	}
}