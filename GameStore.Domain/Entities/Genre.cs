using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Abstract;

namespace GameStore.Domain.Entities
{
    public class Genre : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Genre> SubGenres { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSubGenre { get; set; }

        public Genre()
        {
            SubGenres = new List<Genre>();
        }
    }
}
