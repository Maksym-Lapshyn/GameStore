using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GameStore.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        public int? GameId { get; set; }

        public string GameKey { get; set; }

        [ScriptIgnore]
        public GameViewModel Game { get; set; }

        public int? ParentCommentId { get; set; }

        [ScriptIgnore]
        public CommentViewModel ParentComment { get; set; }

        public IEnumerable<CommentViewModel> ChildComments { get; set; }

        public CommentViewModel()
        {
            ChildComments = new List<CommentViewModel>();
        }
    }
}