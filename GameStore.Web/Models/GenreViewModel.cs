using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class GenreViewModel
	{
		public GenreViewModel()
		{
			ParentGenresData = new List<GenreViewModel>();
		}

		public int Id { get; set; }

		public string NorthwindId { get; set; }

		public bool IsDeleted { get; set; }

		[Required]
		[DisplayName("Genre Name")]
		public string Name { get; set; }

		public List<GenreViewModel> ParentGenresData { get; set; }

		public string ParentGenresInput { get; set; }
	}
}