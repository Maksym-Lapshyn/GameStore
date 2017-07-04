using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Genre : BaseEntity
    {
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Genre> ChildGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
