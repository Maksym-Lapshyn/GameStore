using GameStore.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class GenreViewModel : BaseEntity
	{
		public GenreViewModel()
		{
			ParentGenreData = new List<GenreViewModel>();
		}

		[Required]
		[DisplayName("Name")]
		public string Name { get; set; }

		public List<GenreViewModel> ParentGenreData { get; set; }

		[DisplayName("Parent Genre")]
		public string ParentGenreInput { get; set; }
	}
}