using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GameStore.DAL.Abstract;

namespace GameStore.Services.DTOs
{
	public class CommentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

		public int? GameId { get; set; }

		public virtual GameDto Game { get; set; }

		public int? ParentCommentId { get; set; }

		public CommentDto ParentComment { get; set; }

		public IEnumerable<CommentDto> ChildComments { get; set; }

		public bool IsDeleted { get; set; }

        public CommentDto()
        {
            ChildComments = new List<CommentDto>();
        }
    }
}