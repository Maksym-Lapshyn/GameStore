using System;
using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System.Collections.Generic;
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
		private readonly IMapper _mapper;
		private const int DefaultPageSize = 10;

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
				GenresData = MapGenres(),
				PlatformTypesData = MapPlatformTypes(),
				PublishersData = MapPublishers()
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
			if (model.Filter == null && model.Paginator == null)
			{
				model.Filter = new FilterViewModel
				{
					GenresData = MapGenres(),
					PlatformTypesData = MapPlatformTypes(),
					PublishersData = MapPublishers()
				};

				var totalItems =
					_gameService.GetAll(_mapper.Map<FilterViewModel, FilterDto>(model.Filter)).ToList().Count;
				model.Paginator = new PaginatorViewModel().Initialize(totalItems, 1, DefaultPageSize);
				var gameDtos = _gameService.GetAll(_mapper.Map<FilterViewModel, FilterDto>(model.Filter), 0, DefaultPageSize).ToList();
				model.Games = _mapper.Map<IEnumerable<GameDto>, List<GameViewModel>>(gameDtos);
			}

			else
			{
				//Reinitialize paginator
				var pageSize = model.Paginator.PageSize;
				var pag = model.Paginator.CurrentPage;
				var skip = model.Paginator.PageSize * (model.Paginator.CurrentPage - 1);
				var take = model.Paginator.PageSize;
				var totalItems =
					_gameService.GetAll(_mapper.Map<FilterViewModel, FilterDto>(model.Filter)).ToList().Count;
				model.Paginator.TotalItems = totalItems;
				var gameDtos = _gameService.GetAll(_mapper.Map<FilterViewModel, FilterDto>(model.Filter), skip, take).ToList();
				model.Games = _mapper.Map<IEnumerable<GameDto>, List<GameViewModel>>(gameDtos);
			}

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
			var games = _gameService.GetAll();
			var count = games.Count();

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

		private List<PlatformTypeViewModel> MapPlatformTypes()
		{
			var platformTypes = Mapper.Map<List<PlatformTypeDto>, List<PlatformTypeViewModel>>(_platformTypeService.GetAll().ToList());

			return platformTypes;
		}

		private List<GenreViewModel> MapGenres()
		{
			var genres = Mapper.Map<List<GenreDto>, List<GenreViewModel>>(_genreService.GetAll().ToList());

			return genres;
		}

		private List<PublisherViewModel> MapPublishers()
		{
			var publishers = Mapper.Map<List<PublisherDto>, List<PublisherViewModel>>(_publisherService.GetAll().ToList());

			return publishers;
		}
	}
}