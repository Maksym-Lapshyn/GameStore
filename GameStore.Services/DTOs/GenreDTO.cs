using GameStore.Common.Entities;
using System.Collections.Generic;

namespace GameStore.Services.Dtos
{
	public class GenreDto : BaseEntity
	{
		public GenreDto()
		{
			ParentGenreData = new List<GenreDto>();
		}

		public string Name { get; set; }

		public List<GenreDto> ParentGenreData { get; set; }

		public string ParentGenreInput { get; set; }
	}
}