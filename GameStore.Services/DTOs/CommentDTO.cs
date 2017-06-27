using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GameStore.DAL.Abstract;

namespace GameStore.Services.DTOs
{
	//TODO: Required: Blank line after each method/property
	public class CommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
		[ScriptIgnore] //TODO: Required: Remove this attribute
		public int? GameId { get; set; }
        [ScriptIgnore] //TODO: Required: Remove this attribute
		public virtual GameDto Game { get; set; }
        [ScriptIgnore] //TODO: Required: Remove this attribute
		public int? ParentCommentId { get; set; }
        [ScriptIgnore] //TODO: Required: Remove this attribute
		public virtual CommentDto ParentComment { get; set; } //TODO: Required: Remove 'virtual'
		public virtual ICollection<CommentDto> ChildComments { get; set; } //TODO: Consider: Remove 'virtual' and ICollection to IEnumerable
		public bool IsDeleted { get; set; }

        public CommentDto()
        {
            ChildComments = new List<CommentDto>();
        }
    }
}