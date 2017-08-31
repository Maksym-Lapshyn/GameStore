using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.App_LocalResources;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using GameStore.Web.Infrastructure.Attributes;

namespace GameStore.Web.Controllers.Api
{
	public class GamesController : BaseApiController
	{
		private const int DefaultPageSize = 10;
		private const int DefaultPage = 1;

		private readonly IGameService _gameService;
		private readonly IGenreService _genreService;
		private readonly IPublisherService _publisherService;
		private readonly IPlatformTypeService _platformTypeService;
		private readonly IMapper _mapper;

		public GamesController(IApiAuthentication authentication,
			IGameService gameService,
			IGenreService genreService,
			IPublisherService publisherService,
			IPlatformTypeService platformTypeService,
			IMapper mapper)
			:base(authentication)
		{
			_gameService = gameService;
			_genreService = genreService;
			_publisherService = publisherService;
			_platformTypeService = platformTypeService;
			_mapper = mapper;
		}

		// GET api/<controller>
		public HttpResponseMessage Get(CompositeGamesViewModel model)
		{
			if (!ModelState.IsValid && model.FilterIsChanged)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, CreateError());
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

			return Request.CreateResponse(HttpStatusCode.OK, model.ToJson());
		}

		// GET api/<controller>/5
		public HttpResponseMessage Get(string key)
		{
			if (!_gameService.Contains(key))
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Game with such key does not exist");
			}

			var gameDto = _gameService.GetSingle(CurrentLanguage, key);
			var gameViewModel = _mapper.Map<GameDto, GameViewModel>(gameDto);

			return Request.CreateResponse(HttpStatusCode.OK, gameViewModel.ToJson());
		}

		// POST api/<controller>
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public HttpResponseMessage Post(GameViewModel model)
		{
			CheckIfKeyIsUnique(model);

			if (!ModelState.IsValid)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, CreateError());
			}

			var gameDto = _mapper.Map<GameViewModel, GameDto>(model);
			_gameService.Create(CurrentLanguage, gameDto);

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		// PUT api/<controller>/5
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public HttpResponseMessage Put(GameViewModel model)
		{
			CheckIfKeyIsUnique(model);

			if (!ModelState.IsValid)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, CreateError());
			}

			var gameDto = _mapper.Map<GameViewModel, GameDto>(model);
			_gameService.Update(CurrentLanguage, gameDto);

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		// DELETE api/<controller>/5
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public HttpResponseMessage Delete(string key)
		{
			if (!_gameService.Contains(key))
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Game with such key does not exist");
			}

			_gameService.Delete(key);

			return Request.CreateResponse(HttpStatusCode.OK);
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

		private ErrorViewModel CreateError()
		{
			var error = new ErrorViewModel
			{
				Message = "ModelState contains errors",
				Errors = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
			};

			return error;
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
	}
}