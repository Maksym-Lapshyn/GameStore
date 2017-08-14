using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	[OutputCache(Duration = 60, VaryByHeader = "get;post")]
	public class CommentsController : BaseController
	{
		private readonly ICommentService _commentService;
		private readonly IMapper _mapper;

		public CommentsController(ICommentService commentService,
			IMapper mapper)
		{
			_commentService = commentService;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult NewComment(string key)
		{
			var model = new CompositeCommentsViewModel
			{
				NewComment = new CommentViewModel
				{
					GameKey = key
				},

				Comments = GetComments(key)
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult NewComment(CompositeCommentsViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Comments = GetComments(model.NewComment.GameKey);

				return View(model);
			}

			var dto = _mapper.Map<CommentViewModel, CommentDto>(model.NewComment);
			_commentService.Create(dto);

			model.Comments = GetComments(model.NewComment.GameKey);
			model.NewComment = new CommentViewModel();

			return View(model);
		}

		public ActionResult Delete(int key)
		{
			_commentService.Delete(key);

			return Request.UrlReferrer != null ? RedirectToAction(Request.UrlReferrer.ToString()) : RedirectToAction("ShowAll", "Games");
		}

		private List<CommentViewModel> GetComments(string key)
		{
			var commentDtos = _commentService.GetAll(key);

			return _mapper.Map<IEnumerable<CommentDto>, List<CommentViewModel>>(commentDtos.ToList());
		}
	}
}