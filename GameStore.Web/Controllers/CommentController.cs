using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
    //[OutputCache(Duration = 60, VaryByHeader = "get;post")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService service)
        {
            _commentService = service;
        }

        [HttpPost]
        public ActionResult NewComment(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(commentViewModel);
            }

            var commentDto = Mapper.Map<CommentViewModel, CommentDto>(commentViewModel);
            _commentService.Create(commentDto);

            return RedirectToAction("ListAllComments", new { gameKey = commentViewModel.GameKey });
        }

        [HttpGet]
        public ViewResult ListAllComments(string gameKey)
        {
            var commentDtos = _commentService.GetBy(gameKey);
            var commentViewModels = Mapper.Map<IEnumerable<CommentDto>, IEnumerable<CommentViewModel>>(commentDtos);

            return View(commentViewModels.ToList());
        }
    }
}