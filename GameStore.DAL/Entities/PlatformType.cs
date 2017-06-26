using System.Collections.Generic;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
    public class PlatformType : ISoftDeletable
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public bool IsDeleted { get; set; }

        public PlatformType()
        {
            Games = new List<Game>();
        }
    }
}
