using System.Collections.Generic;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
    public class Genre : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Genre> ChildGenres { get; set; }
        public virtual Genre ParentGenre { get; set; }
        public bool IsDeleted { get; set; }

        public Genre()
        {
            ChildGenres = new List<Genre>();
        }
    }
}
