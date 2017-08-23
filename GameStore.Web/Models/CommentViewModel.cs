using GameStore.Common.Entities;
using GameStore.Web.App_LocalResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class CommentViewModel : BaseEntity
	{
		[Required(ErrorMessageResourceName = "CommentAuthorIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "CommentAuthor", ResourceType = typeof(GlobalResource))]
		public string Name { get; set; }

		[Required(ErrorMessageResourceName = "CommentBodyIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "CommentBody", ResourceType = typeof(GlobalResource))]
		public string Body { get; set; }

		[Display(Name = "ParentCommentAuthor", ResourceType = typeof(GlobalResource))]
		public string ParentCommentAuthor { get; set; }

		[Display(Name = "GameKey", ResourceType = typeof(GlobalResource))]
		public string GameKey { get; set; }

		public int? ParentCommentId { get; set; }

		public List<CommentViewModel> ChildComments { get; set; }
	}
}