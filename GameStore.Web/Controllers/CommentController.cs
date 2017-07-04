﻿using AutoMapper;
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

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult New(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(commentViewModel);
            }

            var commentDto = Mapper.Map<CommentViewModel, CommentDto>(commentViewModel);
            _commentService.Create(commentDto);

            return RedirectToAction("ListAll", new { gameKey = commentViewModel.GameKey });
        }

        [HttpGet]
        public ViewResult ListAll(string gameKey)
        {
            var commentDtos = _commentService.GetSingleBy(gameKey);
            var commentViewModels = Mapper.Map<List<CommentDto>, List<CommentViewModel>>(commentDtos.ToList());

            return View(commentViewModels.ToList());
        }
    }
}