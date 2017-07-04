using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace GameStore.Web.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GenreViewModel> ChildGenres { get; set; }

        [ScriptIgnore]
        public GenreDto ParentGenre { get; set; }
    }
}