using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGenreService
	{
		IEnumerable<GenreDto> GetAll(string language);

		GenreDto GetSingle(string language, string name);

		void Create(string language, GenreDto genreDto);

		void Update(string language, GenreDto genreDto);

		void Delete(string name);

		bool Contains(string language, string name);
	}
}