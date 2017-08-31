using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.App_LocalResources;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
	public class GenresController : BaseApiController
	{
		private readonly IGenreService _genreService;
		private readonly IMapper _mapper;

		public GenresController(IGenreService genreService,
			IMapper mapper, 
			IApiAuthentication authentication)
			:base(authentication)
		{
			_genreService = genreService;
			_mapper = mapper;
		}

		// GET api/<controller>
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Get(string contentType)
		{
			var dtos = _genreService.GetAll(CurrentLanguage);
			var model = _mapper.Map<IEnumerable<GenreDto>, List<GenreViewModel>>(dtos);

			return SerializeResult(model, contentType);
		}

		// GET api/<controller>/5
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Get(string key, string contentType)
		{
			if (!_genreService.Contains(CurrentLanguage, key))
			{
				return Content(HttpStatusCode.BadRequest, "Genre with such name does not exist");
			}

			var dto = _genreService.GetSingle(CurrentLanguage, key);
			var model = _mapper.Map<GenreDto, GenreViewModel>(dto);

			return SerializeResult(model, contentType);
		}

		// POST api/<controller>
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Post(GenreViewModel model)
		{
			CheckIfNameIsUnique(model);

			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var dto = _mapper.Map<GenreViewModel, GenreDto>(model);
			_genreService.Update(CurrentLanguage, dto);

			return Ok();
		}

		// PUT api/<controller>/5
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Put(GenreViewModel model, string contentType)
		{
			if (!_genreService.Contains(CurrentLanguage, model.Name))
			{
				return Content(HttpStatusCode.BadRequest, "Genre with such name does not exist");
			}

			CheckIfNameIsUnique(model);

			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var dto = _mapper.Map<GenreViewModel, GenreDto>(model);
			_genreService.Update(CurrentLanguage, dto);

			return Ok();
		}

		// DELETE api/<controller>/5
		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public IHttpActionResult Delete(string name, string contentType)
		{
			if (!_genreService.Contains(CurrentLanguage, name))
			{
				return Content(HttpStatusCode.BadRequest, "Genre with such name does not exist");
			}

			_genreService.Delete(name);

			return Ok();
		}

		private void CheckIfNameIsUnique(GenreViewModel model)
		{
			if (!_genreService.Contains(CurrentLanguage, model.Name))
			{
				return;
			}

			var existingGenre = _genreService.GetSingle(CurrentLanguage, model.Name);

			if (existingGenre.Id != model.Id)
			{
				ModelState.AddModelError("Name", GlobalResource.GenreWithSuchNameAlreadyExists);
			}
		}
	}
}