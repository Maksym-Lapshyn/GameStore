using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;

namespace GameStore.Web.Controllers
{
    //[OutputCache(Duration = 60, VaryByHeader = "get;post")]
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService service)
        {
            _commentService = service;
        }

        [HttpPost]
        public JsonResult NewComment(string gameKey, CommentDTO comment)
        {
            if (comment.ParentComment != null)
            {
                _commentService.AddCommentToComment(comment);
            }
            else
            {
                _commentService.AddCommentToGame(gameKey, comment);
            }
            
            return null;
        }

        [HttpGet]
        public JsonResult ListAllComments(string gameKey)
        {
            IEnumerable<CommentDTO> comments = _commentService.GetAllCommentsByGameKey(gameKey);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}