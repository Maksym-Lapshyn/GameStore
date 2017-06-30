using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using GameStore.Services.DTOs;

namespace GameStore.Web.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GenreViewModel> ChildGenres { get; set; }

        [ScriptIgnore]
        public GenreDto ParentGenre { get; set; }

        public GenreViewModel()
        {
            ChildGenres = new List<GenreViewModel>();
        }
    }
}