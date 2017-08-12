using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGenreService
	{
		IEnumerable<GenreDto> GetAll();

		GenreDto GetSingle(string name);

		void Create(GenreDto genreDto);

		void Update(GenreDto genreDto);

		void Delete(string name);

		bool Contains(string name);
	}
}