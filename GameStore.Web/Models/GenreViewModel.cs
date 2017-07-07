using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Web.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GenreViewModel> ChildGenres { get; set; }

        public GenreDto ParentGenre { get; set; }
    }
}