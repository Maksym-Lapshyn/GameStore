using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.App_LocalResources;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
	public class PublishersController : BaseApiController
	{
		private readonly IPublisherService _publisherService;
		private readonly IGameService _gameService;
		private readonly IMapper _mapper;

		public PublishersController(IApiAuthentication authentication,
			IPublisherService publisherService,
			IGameService gameService,
			IMapper mapper)
			: base(authentication)
		{
			_publisherService = publisherService;
			_gameService = gameService;
			_mapper = mapper;
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpGet]
		public IHttpActionResult GetAllByGameKey(string key, string contentType)
		{
			if (!_gameService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Game with such key does not exist");
			}

			var dto = _publisherService.GetSingleByGameKey(CurrentLanguage, key);
			var model = _mapper.Map<PublisherDto, PublisherViewModel>(dto);

			return SerializeResult(model, contentType);
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Get(string contentType)
		{
			var dtos = _publisherService.GetAll(CurrentLanguage);
			var model = _mapper.Map<IEnumerable<PublisherDto>, List<PublisherViewModel>>(dtos);

			return SerializeResult(model, contentType);
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Get(string key, string contentType)
		{
			if (!_publisherService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Publisher with such company name does not exist");
			}

			var dto = _publisherService.GetSingle(CurrentLanguage, key);
			var model = _mapper.Map<PublisherDto, PublisherViewModel>(dto);

			return SerializeResult(model, contentType);
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Post(PublisherViewModel model)
		{
			CheckIfCompanyNameIsUnique(model);

			CheckIfDescriptionIsNull(model);

			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var dto = _mapper.Map<PublisherViewModel, PublisherDto>(model);
			_publisherService.Update(CurrentLanguage, dto);

			return Ok();
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Put(string key, PublisherViewModel model)
		{
			if (!_publisherService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Publisher with such company name does not exist");
			}

			CheckIfCompanyNameIsUnique(model);

			CheckIfDescriptionIsNull(model);

			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var dto = _mapper.Map<PublisherViewModel, PublisherDto>(model);
			_publisherService.Create(CurrentLanguage, dto);

			return Ok();
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Delete(string key)
		{
			if (!_publisherService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Publisher with such company name does not exist");
			}

			_publisherService.Delete(key);

			return Ok();
		}

		private void CheckIfCompanyNameIsUnique(PublisherViewModel model)
		{
			if (!_publisherService.Contains(model.CompanyName))
			{
				return;
			}

			var existingPublisher = _publisherService.GetSingle(CurrentLanguage, model.CompanyName);

			if (existingPublisher.Id != model.Id)
			{
				ModelState.AddModelError("CompanyName", GlobalResource.PublisherWithSuchCompanyNameAlreadyExists);
			}
		}

		private void CheckIfDescriptionIsNull(PublisherViewModel model)
		{
			if (model.Description == null)
			{
				ModelState.AddModelError("Description", GlobalResource.DescriptionIsRequired);
			}
		}
	}
}