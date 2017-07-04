using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
    public interface IGenreService
    {
        IEnumerable<GenreDto> GetBy(int gameId);

        IEnumerable<GenreDto> GetAll();
    }
}
