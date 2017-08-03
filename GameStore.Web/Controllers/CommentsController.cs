using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	[OutputCache(Duration = 60, VaryByHeader = "get;post")]
	public class CommentsController : Controller
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
		public ActionResult ListAll(string gameKey)
		{
			var model = new AllCommentsViewModel
			{
				NewComment = new CommentViewModel
				{
					GameKey = gameKey
				},

				GameKey = gameKey,
				Comments = GetComments(gameKey)
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult ListAll(AllCommentsViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Comments = GetComments(model.GameKey);

				return View(model);
			}

			var commentDto = _mapper.Map<CommentViewModel, CommentDto>(model.NewComment);
			_commentService.Create(commentDto);

			model.NewComment = new CommentViewModel();
			model.Comments = GetComments(model.GameKey);

			return View(model);
		}

		private List<CommentViewModel> GetComments(string gameKey)
		{
			var commentDtos = _commentService.GetBy(gameKey);

			return _mapper.Map<IEnumerable<CommentDto>, List<CommentViewModel>>(commentDtos.ToList());
		}
	}
}