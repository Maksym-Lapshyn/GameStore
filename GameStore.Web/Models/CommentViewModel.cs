using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace GameStore.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? GameId { get; set; }
        [ScriptIgnore]
        public GameViewModel Game { get; set; }
        public int? ParentCommentId { get; set; }
        [ScriptIgnore]
        public CommentViewModel ParentComment { get; set; }
        public ICollection<CommentViewModel> ChildComments { get; set; }
        public bool IsDeleted { get; set; }

        public CommentViewModel()
        {
            ChildComments = new List<CommentViewModel>();
        }
    }
}