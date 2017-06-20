using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Entities;
using GameStore.Services.Abstract;

namespace GameStore.Web.Controllers
{
    public class CommentsController : Controller
    {
        private IGameService _iService;

        public CommentsController(IGameService service)
        {
            _iService = service;
        }

        [HttpPost]
        public JsonResult NewComment(string gameKey, Comment comment)
        {
            _iService.AddCommentToGame(gameKey, comment);
            return null;
        }

        [HttpGet]
        public JsonResult ListAllComments(string gameKey)
        {
            IEnumerable<Comment> comments = _iService.GetAllCommentsByGameKey(gameKey);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _iService.Dispose();
            base.Dispose(disposing);
        }
    }
}