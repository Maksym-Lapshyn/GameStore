using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	//[OutputCache(Duration = 60, VaryByHeader = "get;post")]
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;
		private readonly IMapper _mapper;

		public CommentController(ICommentService commentService,
			IMapper mapper)
		{
			_commentService = commentService;
			_mapper = mapper;
		}

		[HttpPost]
		public ActionResult New(CommentViewModel commentViewModel)
		{
			if (!ModelState.IsValid)
			{
				return PartialView(commentViewModel);
			}

			var commentDto = _mapper.Map<CommentViewModel, CommentDto>(commentViewModel);
			_commentService.Create(commentDto);

			return RedirectToAction("ListAll", new { gameKey = commentViewModel.GameKey });
		}

		[HttpGet]
		public ActionResult ListAll(string gameKey)
		{
			var commentDtos = _commentService.GetBy(gameKey);
			var commentViewModels = _mapper.Map<IEnumerable<CommentDto>, List<CommentViewModel>>(commentDtos.ToList());
			var commentListViewModel = new AllCommentsViewModel
			{
				GameId = commentViewModels.First().GameId,
				GameKey = gameKey,
				Comments = commentViewModels
			};

			return View(commentListViewModel);
		}
	}
}