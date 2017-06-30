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

        public GameDto Game { get; set; }

        public int? ParentCommentId { get; set; }

        public CommentDto ParentComment { get; set; }

        public IEnumerable<CommentDto> ChildComments { get; set; }

        public CommentDto()
        {
            ChildComments = new List<CommentDto>();
        }
    }
}