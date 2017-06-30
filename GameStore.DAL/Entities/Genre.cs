using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
	public class Genre : BaseEntity
    {
        [StringLength(450)]
        [Index(IsUnique = true)]
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
