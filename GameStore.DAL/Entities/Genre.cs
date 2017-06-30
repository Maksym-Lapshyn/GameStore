using System.Collections.Generic;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
	public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Genre> ChildGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Genre()
        {
            ChildGenres = new List<Genre>();
            Games = new List<Game>();
        }
    }
}
