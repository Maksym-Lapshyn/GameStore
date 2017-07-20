using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class GameController : Controller
	{
		private const int DefaultPageSize = 10; //TODO Required: Move constants to top
		private const int DefaultPage = 1;
		private static FilterViewModel _filterState;  // TODO Required: Move static after constants
		private readonly IGameService _gameService;
		private readonly IGenreService _genreService;
		private readonly IPlatformTypeService _platformTypeService;
		private readonly IPublisherService _publisherService;
		private readonly IMapper _mapper;

		public GameController(IGameService gameService,
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

		[HttpPost]
		public ActionResult Update(GameDto game)
		{
			_gameService.Edit(game);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		public ActionResult Show(string gameKey)
		{
			var gameDto = _gameService.GetSingleBy(gameKey);
			_gameService.SaveView(gameDto.Id);
			var gameViewModel = _mapper.Map<GameDto, GameViewModel>(gameDto);

			return View(gameViewModel);
		}

		[HttpGet]
		public ActionResult ListAll(AllGamesViewModel model)
		{
			int itemsToSkip, itemsToTake;
 
			if(!ModelState.IsValid && model.FilterIsChanged)//TODO Required: Don't validate fields separately. You should validate all model
			{
				model.FilterIsChanged = false;
				model.Filter.PlatformTypesData = GetPlatformTypes();
				model.Filter.GenresData = GetGenres();
				model.Filter.PublishersData = GetPublishers();
				itemsToSkip = model.PageSize * (model.CurrentPage - 1); //TODO Required: Remove useless '()'
				itemsToTake = model.PageSize;
				model.Games = GetGames(_filterState, itemsToSkip, itemsToTake);

				return View(model);
			}

			if (!ModelState.IsValid && !model.FilterIsChanged) //TODO Required: Don't validate fields separately. You should validate all model
			{
				ModelState.Clear(); //TODO: Why do you need clear ModelState if you don't use it below?
			}   //If I remove this line, I will see validation error for GameName every time I change it,
				//do do not apply filter and move to another changePage, which is not a very user-friendly behaviour

			if (model.Filter == null)
			{
				model.Filter = new FilterViewModel();
				model.CurrentPage = DefaultPage;
				model.PageSize = DefaultPageSize;
				model.TotalItems = _gameService.GetCount();
				model = UpdatePagination(model);
				_filterState = model.Filter;
			}
			else
			{
				if (model.FilterIsChanged)
				{
					_filterState = model.Filter;
					model.CurrentPage = DefaultPage;
					model.TotalItems = _gameService.GetCount(_mapper.Map<FilterViewModel, FilterDto>(_filterState)); // TODO Required: GetAll->Map->CalculateCount Really? Do it simply.
					model = UpdatePagination(model);
					model.FilterIsChanged = false;
				}
			}

			model.TotalPages = (int)Math.Ceiling((decimal)model.TotalItems / model.PageSize);
			model = UpdatePagination(model);
			itemsToSkip = model.PageSize * (model.CurrentPage - 1);
			itemsToTake = model.PageSize;
			model.Games = GetGames(_filterState, itemsToSkip, itemsToTake);
			model.Filter.PlatformTypesData = GetPlatformTypes();
			model.Filter.GenresData = GetGenres();
			model.Filter.PublishersData = GetPublishers();

			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int gameId)
		{
			_gameService.Delete(gameId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[OutputCache(Duration = 60)]
		public ActionResult ShowCount()
		{
			var count = _gameService.GetCount(); // TODO Required: GetAll->Map->CalculateCount Really? Do it simply.

			return PartialView(count);
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

		//TODO Consider: Move private methods to separate Mapper class.
		//TODO: Required: You can't give a method name 'Map....' and invoke service method inside. (stick to this signatures: Type1 Map(Type2), Type1 Get(params/no params))
		private List<GameViewModel> GetGames(FilterViewModel filter, int itemsToSkip, int itemsToTake)
		{
			var filterDto = _mapper.Map<FilterViewModel, FilterDto>(filter);

			return _mapper.Map<IEnumerable<GameDto>, List<GameViewModel>>(_gameService.GetAll(filterDto, itemsToSkip, itemsToTake));
		}

		private List<PlatformTypeViewModel> GetPlatformTypes()
		{
			return Mapper.Map<IEnumerable<PlatformTypeDto>, List<PlatformTypeViewModel>>(_platformTypeService.GetAll());
		}

		private List<GenreViewModel> GetGenres()
		{
			return Mapper.Map<IEnumerable<GenreDto>, List<GenreViewModel>>(_genreService.GetAll());
		}

		private List<PublisherViewModel> GetPublishers()
		{
			return Mapper.Map<IEnumerable<PublisherDto>, List<PublisherViewModel>>(_publisherService.GetAll());
		}
	}
}