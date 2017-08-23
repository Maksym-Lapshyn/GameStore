using GameStore.Common.Entities;
using GameStore.Web.App_LocalResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class GenreViewModel : BaseEntity
	{
		public GenreViewModel()
		{
			ParentGenreData = new List<GenreViewModel>();
		}

		[Required(ErrorMessageResourceName = "GenreNameIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "GenreName", ResourceType = typeof(GlobalResource))]
		public string Name { get; set; }

		public List<GenreViewModel> ParentGenreData { get; set; }

		[Display(Name = "ParentGenre", ResourceType = typeof(GlobalResource))]
		public string ParentGenreInput { get; set; }
	}
}