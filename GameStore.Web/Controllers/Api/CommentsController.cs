using AutoMapper;
using GameStore.Authentification.Abstract;
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
	public class CommentsController : BaseApiController
	{
		private readonly ICommentService _commentService;
		private readonly IMapper _mapper;

		public CommentsController(IApiAuthentication authentication,
			ICommentService commentService,
			IMapper mapper)
			: base(authentication)
		{
			_commentService = commentService;
			_mapper = mapper;
		}

		public IHttpActionResult GetAllByGameKey(string key, string contentType)
		{
			if (!_commentService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments");
			}

			var dtos = _commentService.GetAll(key).ToList();

			if (dtos.Count == 0)
			{
				return Content(HttpStatusCode.BadRequest, "Comments with such game key do not exist");
			}

			var model = _mapper.Map<IEnumerable<CommentDto>, List<CommentViewModel>>(dtos);

			return SerializeResult(model, contentType);
		}

		public IHttpActionResult Get(string key, int id, string contentType)
		{
			if (!_commentService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments");
			}

			if (!_commentService.Contains(id))
			{
				return Content(HttpStatusCode.BadRequest, "Comment with such id does not exist");
			}

			var dto = _commentService.GetSingle(id);

			var model = _mapper.Map<CommentDto, CommentViewModel>(dto);

			return SerializeResult(model, contentType);
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Moderator)]
		public IHttpActionResult Post(string key, CommentViewModel model)
		{
			if (!_commentService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments");
			}

			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var dto = _mapper.Map<CommentViewModel, CommentDto>(model);
			_commentService.Create(dto);

			return Ok();
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Moderator)]
		public IHttpActionResult Put(string key, CommentViewModel model)
		{
			if (!_commentService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments");
			}

			if (!_commentService.Contains(model.Id))
			{
				return Content(HttpStatusCode.BadRequest, "Comment with such id does not exist");
			}

			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var dto = _mapper.Map<CommentViewModel, CommentDto>(model);
			_commentService.Update(dto);

			return Ok();
		}

		[AuthorizeApiUser(AuthorizationMode.Allow, AccessLevel.Moderator)]
		public IHttpActionResult Delete(string key, int id)
		{
			if (!_commentService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Game with such key does not have comments");
			}

			if (!_commentService.Contains(id))
			{
				return Content(HttpStatusCode.BadRequest, "Comment with such id does not exist");
			}

			_commentService.Delete(id);

			return Ok();
		}
	}
}