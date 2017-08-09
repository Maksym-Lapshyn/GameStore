using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGenreService : IService
	{
		IEnumerable<GenreDto> GetAll();
	}
}