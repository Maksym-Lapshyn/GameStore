using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IGenreService : IService<GenreDto>
    {
        IEnumerable<GenreDto> GetBy(int gameId);
    }
}
