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
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class CommentsController : BaseController
	{
		private readonly ICommentService _commentService;
		private readonly IGameService _gameService;
		private readonly IMapper _mapper;

		public CommentsController(ICommentService commentService,
			IGameService gameService,
			IMapper mapper, 
			IAuthentication authentication)
			: base(authentication)
		{
			_commentService = commentService;
			_gameService = gameService;
			_mapper = mapper;
		}

		[AuthorizeUser(AuthorizationMode.Forbid, AccessLevel.Administrator)]
		[HttpGet]
		public ActionResult New(string key)
		{
			var model = new CompositeCommentsViewModel
			{
				NewComment = new CommentViewModel
				{
					Name = User.Identity.Name,
					GameKey = key
				},

				GameIsDeleted = _gameService.GetSingle(CurrentLanguage, key).IsDeleted,
				Comments = GetComments(key)
			};

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Forbid, AccessLevel.Administrator)]
		[HttpPost]
		public ActionResult New(CompositeCommentsViewModel model)
		{
			if (model.GameIsDeleted)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			}

			if (!ModelState.IsValid)
			{
				model.Comments = GetComments(model.NewComment.GameKey);

				return View(model);
			}

			var dto = _mapper.Map<CommentViewModel, CommentDto>(model.NewComment);
			_commentService.Create(dto);

			return RedirectToAction("New", new { key = model.NewComment.GameKey });
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Moderator)]
		public ActionResult Delete(int key)
		{
			_commentService.Delete(key);

			return RedirectToAction("New", "Comments", new { key });
		}

		private List<CommentViewModel> GetComments(string key)
		{
			var commentDtos = _commentService.GetAll(key);

			return _mapper.Map<IEnumerable<CommentDto>, List<CommentViewModel>>(commentDtos.ToList());
		}
	}
}