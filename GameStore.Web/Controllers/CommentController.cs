using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;
using GameStore.Web.Infrastructure.Attributes;

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
        public ActionResult NewComment(CommentDTO comment)
        {
            if (comment.ParentComment != null)
            {
                _commentService.AddCommentToComment(comment);
            }
            else
            {
                _commentService.AddCommentToGame(comment);
            }

            return Redirect("https://youtube.com");
        }

        [HttpGet]
        public JsonResult ListAllComments(string gameKey)
        {
            IEnumerable<CommentDTO> comments = _commentService.GetAllCommentsByGameKey(gameKey);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}