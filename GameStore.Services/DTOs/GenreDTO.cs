﻿using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class GenreDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<GenreDto> ChildGenres { get; set; }

		public GenreDto ParentGenre { get; set; }

		public GenreDto()
		{
			ChildGenres = new List<GenreDto>();
		}
	}
}
