using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Genre
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
