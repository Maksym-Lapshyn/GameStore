using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using GameStore.Services.DTOs;
using GameStore.Services.Abstract;

namespace GameStore.Web.Controllers
{
    [OutputCache(Duration = 60, VaryByHeader = "get;post")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService service)
        {
            _commentService = service;
        }

        [HttpPost]
        public ActionResult NewComment(CommentDto comment)
        {
            if (comment.ParentCommentId != null)
            {
                _commentService.AddCommentToComment(comment);
            }
            else
            {
                _commentService.AddCommentToGame(comment);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public JsonResult ListAllComments(string gameKey)
        {
            IEnumerable<CommentDto> comments = _commentService.GetAllCommentsByGameKey(gameKey);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}