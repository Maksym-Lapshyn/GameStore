using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace GameStore.Services.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? GameId { get; set; }
        [ScriptIgnore]
        public virtual GameDto Game { get; set; }
        public int? ParentCommentId { get; set; }
        [ScriptIgnore]
        public virtual CommentDto ParentComment { get; set; }
        public virtual ICollection<CommentDto> ChildComments { get; set; }
        public bool IsDeleted { get; set; }

        public CommentDto()
        {
            ChildComments = new List<CommentDto>();
        }
    }
}